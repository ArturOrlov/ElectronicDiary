using AutoMapper;
using ElectronicDiary.Dto.PerformanceRating;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class PerformanceRatingService : IPerformanceRatingService
{
    private readonly IPerformanceRatingRepository _performanceRatingRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public PerformanceRatingService(IPerformanceRatingRepository performanceRatingRepository,
        ISubjectRepository subjectRepository,
        IUserService userService,
        IMapper mapper)
    {
        _performanceRatingRepository = performanceRatingRepository;
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

    public async Task<BaseResponse<GetPerformanceRatingDto>> CreatePerformanceRatingAsync(CreatePerformanceRatingDto request)
    {
        var response = new BaseResponse<GetPerformanceRatingDto>();

        var teacher = await _userService.GetByIdAsync(request.TeacherId);

        if (teacher == null)
        {
            response.IsError = true;
            response.Description = $"Учитель с id - {request.TeacherId} не найден";
            return response;
        }

        var student = await _userService.GetByIdAsync(request.StudentId);

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

        if (request.TeacherId.HasValue)
        {
            var teacher = await _userService.GetByIdAsync((int)request.TeacherId);

            if (teacher == null)
            {
                response.IsError = true;
                response.Description = $"Учитель с id - {request.TeacherId} не найден";
                return response;
            }

            performanceRating.TeacherId = (int)request.TeacherId;
        }

        if (request.StudentId.HasValue)
        {
            var student = await _userService.GetByIdAsync((int)request.StudentId);

            if (student == null)
            {
                response.IsError = true;
                response.Description = $"Ученик с id - {request.StudentId} не найден";
                return response;
            }

            performanceRating.StudentId = (int)request.StudentId;
        }

        if (request.SubjectId.HasValue)
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