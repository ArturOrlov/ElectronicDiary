using AutoMapper;
using ElectronicDiary.Dto.PerformanceRating;
using ElectronicDiary.Dto.Report;
using ElectronicDiary.Dto.Subject;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class PerformanceRatingService : IPerformanceRatingService
{
    private readonly IPerformanceRatingRepository _performanceRatingRepository;
    private readonly IUserClassRepository _userClassRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public PerformanceRatingService(IPerformanceRatingRepository performanceRatingRepository, 
        IUserClassRepository userClassRepository,
        ISubjectRepository subjectRepository, 
        IUserService userService,
        IMapper mapper)
    {
        _performanceRatingRepository = performanceRatingRepository;
        _userClassRepository = userClassRepository;
        _subjectRepository = subjectRepository;
        _userService = userService;
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

    public async Task<BaseResponse<List<GetPerformanceRatingDto>>> GetPerformanceRatingByPaginationAsync(BasePagination request)
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

    public async Task<BaseResponse<List<ResponsePerformanceRatingReportDto>>> GetPerformanceRatingReportAsync(GetPerformanceRatingReportDto request)
    {
        var response = new BaseResponse<List<ResponsePerformanceRatingReportDto>>();

        var user = await _userService.GetByIdAsync(request.UserId);

        if (user.Data == null)
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

    public async Task<BaseResponse<GetPerformanceRatingDto>> CreatePerformanceRatingAsync(CreatePerformanceRatingDto request)
    {
        var response = new BaseResponse<GetPerformanceRatingDto>();

        var teacher = await _userService.GetByIdAsync(request.TeacherId);

        if (teacher.Data == null)
        {
            response.IsError = true;
            response.Description = $"Учитель с id - {request.TeacherId} не найден";
            return response;
        }

        var student = await _userService.GetByIdAsync(request.StudentId);

        if (student.Data == null)
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

    public async Task<BaseResponse<GetPerformanceRatingDto>> UpdatePerformanceRatingByIdAsync(int performanceRatingId, UpdatePerformanceRatingDto request)
    {
        var response = new BaseResponse<GetPerformanceRatingDto>();

        var performanceRating = await _performanceRatingRepository.GetByIdAsync(performanceRatingId);

        if (performanceRating == null)
        {
            response.IsError = true;
            response.Description = $"Оценка по предмету с id - {performanceRatingId} не найдена";
            return response;
        }

        if (request.TeacherId.HasValue  && request.TeacherId != 0)
        {
            var teacher = await _userService.GetByIdAsync((int)request.TeacherId);

            if (teacher.Data == null)
            {
                response.IsError = true;
                response.Description = $"Учитель с id - {request.TeacherId} не найден";
                return response;
            }

            performanceRating.TeacherId = (int)request.TeacherId;
        }

        if (request.StudentId.HasValue && request.StudentId != 0)
        {
            var student = await _userService.GetByIdAsync((int)request.StudentId);

            if (student.Data == null)
            {
                response.IsError = true;
                response.Description = $"Ученик с id - {request.StudentId} не найден";
                return response;
            }

            performanceRating.StudentId = (int)request.StudentId;
            
            var userClass = _userClassRepository.Get(uc => uc.UserId == student.Data.Id).FirstOrDefault();
        
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