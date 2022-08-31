using AutoMapper;
using ElectronicDiary.Dto.PerformanceRating;
using ElectronicDiary.Dto.Report;
using ElectronicDiary.Dto.Subject;
using ElectronicDiary.Dto.User;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;
using ExcelLibrary.SpreadSheet;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDiary.Services;

public class PerformanceRatingService : IPerformanceRatingService
{
    private readonly IPerformanceRatingRepository _performanceRatingRepository;
    private readonly ISchoolClassRepository _schoolClassRepository;
    private readonly IUserClassRepository _userClassRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public PerformanceRatingService(IPerformanceRatingRepository performanceRatingRepository, 
        ISchoolClassRepository schoolClassRepository,
        IUserClassRepository userClassRepository,
        IUserInfoRepository userInfoRepository,
        IUserRepository userRepository,
        ISubjectRepository subjectRepository,
        IMapper mapper)
    {
        _performanceRatingRepository = performanceRatingRepository;
        _schoolClassRepository = schoolClassRepository;
        _userClassRepository = userClassRepository;
        _userInfoRepository = userInfoRepository;
        _userRepository = userRepository;
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }
    
    public async Task<BaseResponse<GetPerformanceRatingDto>> GetPerformanceRatingByIdAsync(int performanceRatingId)
    {
        var response = new BaseResponse<GetPerformanceRatingDto>();

        var performanceRating = await _performanceRatingRepository.GetByIdAsync(performanceRatingId);

        if (performanceRating == null)
        {
            response.IsError = true;
            response.Description = $"Оценка по предмету с id - {performanceRatingId} не найдена";
            return response;
        }

        var mapPerformanceRating = _mapper.Map<GetPerformanceRatingDto>(performanceRating);

        response.Data = mapPerformanceRating;
        return response;
    }

    public async Task<BaseResponse<List<GetPerformanceRatingDto>>> GetPerformanceRatingByPaginationAsync(
        BasePagination request)
    {
        var response = new BaseResponse<List<GetPerformanceRatingDto>>();

        var performanceRatings = _performanceRatingRepository.Get(_ => true, request);

        if (performanceRatings == null || !performanceRatings.Any())
        {
            return response;
        }

        var mapPerformanceRating = _mapper.Map<List<GetPerformanceRatingDto>>(performanceRatings);

        response.Data = mapPerformanceRating;
        return response;
    }

    public async Task<BaseResponse<List<GetPerformanceRatingDto>>> GetPerformanceRatingBySelfAsync(UserDataDto userData)
    {
        var response = new BaseResponse<List<GetPerformanceRatingDto>>();

        if (Constants.Role.Parent != userData.Role || Constants.Role.Student != userData.Role)
        {
            response.IsError = true;
            response.Description = $"Пользователь с ролью - {userData.Role} не может получить свои оценки";
            return response;
        }

        List<GetPerformanceRatingDto> mapPerformanceRating;

        if (Constants.Role.Parent == userData.Role)
        {
            var userInfo = await _userInfoRepository.GetByUserId(int.Parse(userData.Id));
            var parentChild = _userRepository.Get(u => userInfo.ChildrenUserId.Contains(u.Id));

            var performanceRatings = _performanceRatingRepository
                .Get(pr => parentChild.Select(u => u.Id).Contains(pr.StudentId))
                .ToList();

            if (!performanceRatings.Any())
            {
                return response;
            }

            performanceRatings.Sort((x, y) => x.Id.CompareTo(y.Id));

            mapPerformanceRating = _mapper.Map<List<GetPerformanceRatingDto>>(performanceRatings);
        }
        else
        {
            var ratings = _performanceRatingRepository.Get(pr => pr.StudentId == int.Parse(userData.Id));

            if (ratings == null || !ratings.Any())
            {
                return response;
            }

            mapPerformanceRating = _mapper.Map<List<GetPerformanceRatingDto>>(ratings);
        }

        response.Data = mapPerformanceRating;
        return response;
    }

    public async Task<BaseResponse<List<ResponsePerformanceRatingReportDto>>> GetPerformanceRatingReportAsync(
        GetPerformanceRatingReportDto request)
    {
        var response = new BaseResponse<List<ResponsePerformanceRatingReportDto>>();

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            response.IsError = true;
            response.Description = $"Ученик с id - {request.UserId} не найден";
            return response;
        }

        // todo Проверка на роль

        // -------------------

        //
        var subjects = await _subjectRepository.GetRangeAsync();

        if (!subjects.ToList().Any())
        {
            response.IsError = true;
            response.Description = $"Учитель с id - {request.UserId} не найден";
            return response;
        }

        var performanceRatings = _performanceRatingRepository.Get(pr => pr.StudentId == request.UserId
                                                                        && pr.CreatedAt.Year == request.Year).ToList();

        if (!performanceRatings.Any())
        {
            response.IsError = true;
            response.Description = $"Оценок у ученика с id - {request.UserId} в указанный год - {request.Year} не найдено";
            return response;
        }

        var report = new List<ResponsePerformanceRatingReportDto>();

        foreach (var performanceRating in performanceRatings)
        {
            var subject = subjects.FirstOrDefault(s => s.Id == performanceRating.SubjectId);

            report.Add(new ResponsePerformanceRatingReportDto()
            {
                Rating = _mapper.Map<GetPerformanceRatingBaseDto>(performanceRating),
                Subject = _mapper.Map<GetSubjectDto>(subject),
            });
        }

        response.Data = report;
        return response;
    }

    public async Task<BaseResponse<FileContentResult>> GetReportPerformanceRatingBySelfAsync(UserDataDto userData)
    {
        var response = new BaseResponse<FileContentResult>();

        var performanceRating = await GetPerformanceRatingBySelfAsync(userData);

        if (performanceRating.IsError)
        {
            response.IsError = true;
            response.Description = performanceRating.Description;
            return response;
        }
        
        var result = await PerformanceRatingInExcel(performanceRating.Data);

        response.Data = ReturnResult(result, "Расписание");
        return response;
    }
    
    private async Task<MemoryStream> PerformanceRatingInExcel(List<GetPerformanceRatingDto> performanceRatings)
    {
        var workbook = new Workbook();
        var worksheet = new Worksheet("First Sheet")
        {
            Cells =
            {
                [0, 0] = new Cell("Класс"),
                [1, 0] = new Cell("Предмет"),
                [2, 0] = new Cell("Оценка")
            }
        };

        var studentIds = _userRepository.Get(u => performanceRatings.Select(pr => pr.StudentId).Contains(u.Id));
        var userClasses = _userClassRepository.Get(uc => studentIds.Select(u => u.Id).Contains(uc.UserId));
        var subjectIds = _subjectRepository.Get(s => performanceRatings.Select(pr => pr.SubjectId).Contains(s.Id));

        for (var i = 0; i < performanceRatings.Count; i++)
        {
            var performanceRating = performanceRatings[i];
            
            var userClass = await _userClassRepository.GetByUserId(userClasses.FirstOrDefault(uc => uc.UserId == performanceRating.StudentId)!.UserId);
            var schoolClass = _schoolClassRepository.Get(sc => sc.Id == userClass.SchoolClassId).FirstOrDefault();
            var number = DateTime.Now.Year - schoolClass!.ClassCreateTime.Year + 1;

            worksheet.Cells[i + 1, 0] = new Cell($"{number}{schoolClass.Symbol}");
            worksheet.Cells[i + 1, 1] = new Cell($"{subjectIds.Select(s => s.Id == performanceRating.SubjectId)}");
            worksheet.Cells[i + 1, 2] = new Cell($"{performanceRating.Valuation}");  
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

    public async Task<BaseResponse<GetPerformanceRatingDto>> CreatePerformanceRatingAsync(
        CreatePerformanceRatingDto request)
    {
        var response = new BaseResponse<GetPerformanceRatingDto>();

        var teacher = await _userRepository.GetByIdAsync(request.TeacherId);

        if (teacher == null)
        {
            response.IsError = true;
            response.Description = $"Учитель с id - {request.TeacherId} не найден";
            return response;
        }

        var student = await _userRepository.GetByIdAsync(request.StudentId);

        if (student == null)
        {
            response.IsError = true;
            response.Description = $"Ученик с id - {request.StudentId} не найден";
            return response;
        }

        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Предмет с id - {request.SubjectId} не найден";
            return response;
        }

        if (request.Valuation is <= 0 or > 5)
        {
            response.IsError = true;
            response.Description = $"Оценка - {request.Valuation} установлена неверно";
            return response;
        }

        var performanceRating = _mapper.Map<PerformanceRating>(request);

        var userClass = _userClassRepository.Get(uc => uc.UserId == performanceRating.StudentId).FirstOrDefault();

        if (userClass == null)
        {
            response.IsError = true;
            response.Description = $"Пользователь с id - {request.StudentId} не находится в школьном классе";
            return response;
        }

        performanceRating.SchoolClassId = userClass.SchoolClassId;

        await _performanceRatingRepository.CreateAsync(performanceRating);

        var mapPerformanceRating = _mapper.Map<GetPerformanceRatingDto>(performanceRating);

        response.Data = mapPerformanceRating;
        return response;
    }

    public async Task<BaseResponse<GetPerformanceRatingDto>> UpdatePerformanceRatingByIdAsync(int performanceRatingId,
        UpdatePerformanceRatingDto request)
    {
        var response = new BaseResponse<GetPerformanceRatingDto>();

        var performanceRating = await _performanceRatingRepository.GetByIdAsync(performanceRatingId);

        if (performanceRating == null)
        {
            response.IsError = true;
            response.Description = $"Оценка по предмету с id - {performanceRatingId} не найдена";
            return response;
        }

        if (request.TeacherId.HasValue && request.TeacherId != 0)
        {
            var teacher = await _userRepository.GetByIdAsync((int)request.TeacherId);

            if (teacher == null)
            {
                response.IsError = true;
                response.Description = $"Учитель с id - {request.TeacherId} не найден";
                return response;
            }

            performanceRating.TeacherId = (int)request.TeacherId;
        }

        if (request.StudentId.HasValue && request.StudentId != 0)
        {
            var student = await _userRepository.GetByIdAsync((int)request.StudentId);

            if (student == null)
            {
                response.IsError = true;
                response.Description = $"Ученик с id - {request.StudentId} не найден";
                return response;
            }

            performanceRating.StudentId = (int)request.StudentId;

            var userClass = _userClassRepository.Get(uc => uc.UserId == student.Id).FirstOrDefault();

            if (userClass == null)
            {
                response.IsError = true;
                response.Description = $"Пользователь с id - {request.StudentId} не находится в школьном классе";
                return response;
            }

            performanceRating.SchoolClassId = userClass.SchoolClassId;
        }

        if (request.SubjectId.HasValue && request.SubjectId != 0)
        {
            var subject = await _subjectRepository.GetByIdAsync((int)request.SubjectId);

            if (subject == null)
            {
                response.IsError = true;
                response.Description = $"Предмет с id - {request.SubjectId} не найден";
                return response;
            }

            performanceRating.SubjectId = (int)request.SubjectId;
        }

        if (request.Valuation.HasValue)
        {
            if (request.Valuation is <= 0 or > 5)
            {
                response.IsError = true;
                response.Description = $"Оценка - {request.Valuation} установлена неверно";
                return response;
            }

            performanceRating.Valuation = (uint)request.Valuation;
        }

        performanceRating.UpdatedAt = DateTimeOffset.Now;
        await _performanceRatingRepository.UpdateAsync(performanceRating);

        var mapPerformanceRating = _mapper.Map<GetPerformanceRatingDto>(performanceRating);

        response.Data = mapPerformanceRating;
        return response;
    }

    public async Task<BaseResponse<string>> DeletePerformanceRatingByIdAsync(int performanceRatingId)
    {
        var response = new BaseResponse<string>();

        var performanceRating = await _performanceRatingRepository.GetByIdAsync(performanceRatingId);

        if (performanceRating == null)
        {
            response.IsError = true;
            response.Description = $"Оценка по предмету с id - {performanceRatingId} не найдена";
            return response;
        }

        await _performanceRatingRepository.DeleteAsync(performanceRating);

        response.Data = "Удалено";
        return response;
    }
}