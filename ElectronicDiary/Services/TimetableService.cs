using AutoMapper;
using ElectronicDiary.Context;
using ElectronicDiary.Dto.Timetable;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;
using ExcelLibrary.SpreadSheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDiary.Services;

public class TimetableService : ITimetableService
{
    private readonly ElectronicDiaryDbContext _context;
    private readonly ITimetableRepository _timetableRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ISchoolClassRepository _schoolClassRepository;
    private readonly IMapper _mapper;

    public TimetableService(ITimetableRepository timetableRepository,
        ISubjectRepository subjectRepository,
        ISchoolClassRepository schoolClassRepository,
        IMapper mapper, ElectronicDiaryDbContext context)
    {
        _timetableRepository = timetableRepository;
        _subjectRepository = subjectRepository;
        _schoolClassRepository = schoolClassRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<BaseResponse<GetTimetableDto>> GetTimetableByIdAsync(int timetableId)
    {
        var response = new BaseResponse<GetTimetableDto>();

        var timetable = await _timetableRepository.GetByIdAsync(timetableId);

        if (timetable == null)
        {
            response.Description = $"Расписание с id - {timetableId} не найдено";
            response.IsError = true;
            return response;
        }

        var mapTimetable = _mapper.Map<GetTimetableDto>(timetable);

        response.Data = mapTimetable;
        return response;
    }

    public async Task<BaseResponse<List<GetTimetableDto>>> GetTimetableByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetTimetableDto>>();

        var timetables = _timetableRepository.Get(_ => true, request);

        if (timetables == null || !timetables.Any())
        {
            return response;
        }

        var mapSubject = _mapper.Map<List<GetTimetableDto>>(timetables);

        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<List<GetTimetableDto>>> GetTimetableByDateAsync(DateTimeOffset date)
    {
        var response = new BaseResponse<List<GetTimetableDto>>();

        var timetables = _timetableRepository.Get(t => t.StartedAt.Date == date.Date);

        if (timetables == null || !timetables.Any())
        {
            return response;
        }

        var mapSubject = _mapper.Map<List<GetTimetableDto>>(timetables);

        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<FileContentResult>> GetTimetableByDateExcelAsync(DateTimeOffset date)
    {
        var response = new BaseResponse<FileContentResult>();

        if (date == default)
        {
            date = DateTimeOffset.Now.Date;
        }

        var timetables = await (from t in _context.Timetable
            join s in _context.Subject on t.SubjectId equals s.Id
            join sc in _context.SchoolClass on t.SchoolClassId equals sc.Id
            where t.StartedAt.Date == date.Date
            select new GetTimetableExcelDto
            {
                Id = t.Id,
                StartedAt = t.StartedAt,
                LessonDuration = t.LessonDuration,
                BreakDuration = t.BreakDuration,
                SubjectId = t.SubjectId,
                SubjectName = s.Name,
                SchoolClassId = t.SchoolClassId,
                SchoolClassName = sc.Symbol,
                ClassCreateTime = sc.ClassCreateTime
            }).ToListAsync();

        var result = await TimetableInExcel(timetables);

        response.Data = ReturnResult(result, "Расписание");
        return response;
    }

    public async Task<BaseResponse<GetTimetableDto>> CreateTimetableAsync(CreateTimetableDto request)
    {
        var response = new BaseResponse<GetTimetableDto>();

        var checkTimetable = _timetableRepository.Get(t =>
                t.SubjectId == request.SubjectId &&
                t.LessonDuration == request.LessonDuration &&
                t.BreakDuration == request.BreakDuration &&
                t.SchoolClassId == request.SchoolClassId &&
                t.StartedAt == request.StartedAt.AddSeconds(-request.StartedAt.Second))
            .FirstOrDefault();

        if (checkTimetable != null)
        {
            response.IsError = true;
            response.Description = $"Расписание переданными данными уже есть";
            return response;
        }

        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Предмета с id - {request.SubjectId} не найден";
            return response;
        }

        var schoolClass = await _schoolClassRepository.GetByIdAsync(request.SchoolClassId);

        if (schoolClass == null)
        {
            response.IsError = true;
            response.Description = $"Класса с id - {request.SchoolClassId} не найден";
            return response;
        }

        if (request.StartedAt.Date < DateTimeOffset.Now.Date)
        {
            response.IsError = true;
            response.Description = $"Установить расписание - {request.StartedAt.Date} на прошедшие дни нельзя";
            return response;
        }

        var timetable = _mapper.Map<Timetable>(request);

        await _timetableRepository.CreateAsync(timetable);

        var mapSubject = _mapper.Map<GetTimetableDto>(timetable);

        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<GetTimetableDto>> UpdateTimetableByIdAsync(int timetableId, UpdateTimetableDto request)
    {
        var response = new BaseResponse<GetTimetableDto>();

        var timetable = await _timetableRepository.GetByIdAsync(timetableId);

        if (timetable == null)
        {
            response.IsError = true;
            response.Description = $"Расписание с id - {timetableId} не найдено";
            return response;
        }

        if (request.SubjectId.HasValue && request.SubjectId != 0)
        {
            var subject = await _subjectRepository.GetByIdAsync((int)request.SubjectId);

            if (subject == null)
            {
                response.IsError = true;
                response.Description = $"Предмета с id - {request.SubjectId} не найден";
                return response;
            }

            timetable.SubjectId = (int)request.SubjectId;
        }

        if (request.SchoolClassId.HasValue && request.SchoolClassId != 0)
        {
            var schoolClass = await _schoolClassRepository.GetByIdAsync((int)request.SchoolClassId);

            if (schoolClass == null)
            {
                response.IsError = true;
                response.Description = $"Класса с id - {request.SchoolClassId} не найден";
                return response;
            }

            timetable.SchoolClassId = (int)request.SchoolClassId;
        }

        if (request.StartedAt.HasValue)
        {
            if (request.StartedAt.Value.Date < DateTimeOffset.Now.Date)
            {
                response.IsError = true;
                response.Description = $"Установить расписание - {request.StartedAt.Value.Date} на прошедшие дни нельзя";
                return response;
            }

            timetable.StartedAt = (DateTimeOffset)request.StartedAt;
        }

        timetable.UpdatedAt = DateTimeOffset.Now;
        await _timetableRepository.UpdateAsync(timetable);

        var mapTimetable = _mapper.Map<GetTimetableDto>(timetable);

        response.Data = mapTimetable;
        return response;
    }

    public async Task<BaseResponse<string>> DeleteTimetableByIdAsync(int timetableId)
    {
        var response = new BaseResponse<string>();

        var subject = await _timetableRepository.GetByIdAsync(timetableId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Предмет с id - {timetableId} не найден";
            return response;
        }

        await _timetableRepository.DeleteAsync(subject);

        response.Data = "Удалено";
        return response;
    }

    private async Task<MemoryStream> TimetableInExcel(List<GetTimetableExcelDto> timetables)
    {
        var workbook = new Workbook();
        var worksheet = new Worksheet("First Sheet");

        timetables.Sort((x, y) => DateTimeOffset.Compare(x.StartedAt, y.StartedAt));

        worksheet.Cells[0, 0] = new Cell("Класс");
        worksheet.Cells[1, 0] = new Cell("Время");
        worksheet.Cells[2, 0] = new Cell("Предмет");

        for (var i = 0; i < timetables.Count; i++)
        {
            var timetable = timetables[i];

            var number = DateTime.Now.Year - timetable.ClassCreateTime.Year + 1;

            worksheet.Cells[i + 1, 0] = new Cell($"{number}{timetable.SchoolClassName}");
            worksheet.Cells[i + 1, 1] = new Cell($"{timetable.StartedAt}");
            worksheet.Cells[i + 1, 2] = new Cell($"{timetable.SubjectName}");
        }

        workbook.Worksheets.Add(worksheet);

        using var stream = new MemoryStream();
        workbook.SaveToStream(stream);
        stream.Flush();
        return stream;
    }

    private FileContentResult ReturnResult(MemoryStream stream, string fileName)
    {
        return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"{fileName}_{DateTime.UtcNow.ToShortDateString()}.xlsx"
        };
    }
}