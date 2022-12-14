using AutoMapper;
using ElectronicDiary.Dto.Report;
using ElectronicDiary.Dto.SchoolClass;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class SchoolClassService : ISchoolClassService
{
    private readonly ISchoolClassRepository _schoolClassRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public SchoolClassService(ISchoolClassRepository schoolClassRepository, 
        ISubjectRepository subjectRepository,
        IMapper mapper)
    {
        _schoolClassRepository = schoolClassRepository;
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetSchoolClassDto>> GetSchoolClassByIdAsync(int schoolClassIdId)
    {
        var response = new BaseResponse<GetSchoolClassDto>();

        var schoolClass = await _schoolClassRepository.GetByIdAsync(schoolClassIdId);

        if (schoolClass == null)
        {
            response.IsError = true;
            response.Description = $"Класс с id - {schoolClassIdId} не найден";
            return response;
        }

        var mapSchoolClass = _mapper.Map<GetSchoolClassDto>(schoolClass);

        response.Data = mapSchoolClass;
        return response;
    }

    public async Task<BaseResponse<List<GetSchoolClassDto>>> GetSchoolClassByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetSchoolClassDto>>();

        var schoolClass = _schoolClassRepository.Get(_ => true, request);

        if (schoolClass == null || !schoolClass.Any())
        {
            return response;
        }

        var mapSchoolClass = _mapper.Map<List<GetSchoolClassDto>>(schoolClass);

        response.Data = mapSchoolClass;
        return response;
    }

    public async Task<BaseResponse<List<GetSchoolClassDto>>> GetSchoolClassReportAsync(GetSchoolClassReportDto request)
    {
        var response = new BaseResponse<List<GetSchoolClassDto>>();
        
        var schoolClasses = _schoolClassRepository.Get(sc => request.SchoolClassId.Contains(sc.Id));

        if (!schoolClasses.Any())
        {
            response.IsError = true;
            response.Description = $"Классы с id - {request.SchoolClassId} не найдены";
            return response;
        }
        
        var subjects = _subjectRepository.Get(s => request.SubjectId.Contains(s.Id));

        if (!subjects.Any())
        {
            response.IsError = true;
            response.Description = $"Предметы с id - {request.SubjectId} не найдены";
            return response;
        }
        
        //todo добавить сбор данных
        //
        //
        //
        
        response.Data = new List<GetSchoolClassDto>();
        return response;
    }

    public async Task<BaseResponse<GetSchoolClassDto>> CreateSchoolClassAsync(CreateSchoolClassDto request)
    {
        var response = new BaseResponse<GetSchoolClassDto>();

        if (string.IsNullOrEmpty(request.Symbol))
        {
            response.IsError = true;
            response.Description = "Описание домашнего задание отсутствует";
            return response;
        }

        var result = CheckSchoolClassYear(request.Symbol, request.ClassCreateTime);

        if (result.Item2 == true)
        {
            response.IsError = true;
            response.Description = result.Item1;
            return response;
        }

        var newSchoolClass = _mapper.Map<SchoolClass>(request);

        await _schoolClassRepository.CreateAsync(newSchoolClass);

        var mapSchoolClass = _mapper.Map<GetSchoolClassDto>(newSchoolClass);

        response.Data = mapSchoolClass;
        return response;
    }

    public async Task<BaseResponse<GetSchoolClassDto>> UpdateSchoolClassByIdAsync(int schoolClassIdId,
        UpdateSchoolClassDto request)
    {
        var response = new BaseResponse<GetSchoolClassDto>();

        var schoolClass = await _schoolClassRepository.GetByIdAsync(schoolClassIdId);

        if (schoolClass == null)
        {
            response.IsError = true;
            response.Description = $"Класс с id - {schoolClassIdId} не найден";
            return response;
        }

        if (!string.IsNullOrEmpty(request.Symbol))
        {
            schoolClass.Symbol = request.Symbol;
        }

        if (request.ClassCreateTime.HasValue)
        {
            if (string.IsNullOrEmpty(schoolClass.Symbol))
            {
                request.Symbol = schoolClass.Symbol;
            }

            var result = CheckSchoolClassYear(request.Symbol, (DateTimeOffset)request.ClassCreateTime);

            if (result.Item2 == true)
            {
                response.IsError = true;
                response.Description = result.Item1;
                return response;
            }

            schoolClass.CreatedAt = (DateTimeOffset)request.ClassCreateTime;
        }

        schoolClass.UpdatedAt = DateTimeOffset.Now;
        await _schoolClassRepository.UpdateAsync(schoolClass);

        var mapSchoolClass = _mapper.Map<GetSchoolClassDto>(schoolClass);

        response.Data = mapSchoolClass;
        return response;
    }

    public async Task<BaseResponse<string>> DeleteSchoolClassByIdAsync(int schoolClassIdId)
    {
        var response = new BaseResponse<string>();

        var schoolClass = await _schoolClassRepository.GetByIdAsync(schoolClassIdId);

        if (schoolClass == null)
        {
            response.IsError = true;
            response.Description = $"Класс с id - {schoolClassIdId} не найден";
            return response;
        }

        await _schoolClassRepository.DeleteAsync(schoolClass);

        response.Data = "Удалено";
        return response;
    }

    private (string, bool) CheckSchoolClassYear(string symbol, DateTimeOffset classCreateTime)
    {
        var schoolClasses = _schoolClassRepository.Get(s => s.Symbol == symbol).ToList();

        if (schoolClasses.Any())
        {
            int i = 0;

            var newTime = classCreateTime - DateTimeOffset.Now;
            var newTimeDays = newTime.Days;

            if (newTimeDays < 0)
            {
                newTimeDays = Math.Abs(newTimeDays);
            }

            foreach (var oldClass in schoolClasses)
            {
                var oldTime = oldClass.ClassCreateTime - DateTimeOffset.Now;
                var oldTimeDays = oldTime.Days;

                if (oldTimeDays < 0)
                {
                    oldTimeDays = Math.Abs(oldTimeDays);
                }

                var different = oldTimeDays - newTimeDays;

                if (different < 0)
                {
                    different = Math.Abs(different);
                }

                if (different <= 365)
                {
                    i++;
                }
            }

            if (i > 0)
            {
                return ($"Класс {symbol} с датой {classCreateTime} уже есть", true);
            }
        }

        return ("Совпадений не обнаружено", false);
    }
}