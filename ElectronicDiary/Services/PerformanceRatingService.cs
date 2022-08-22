using AutoMapper;
using ElectronicDiary.Dto.PerformanceRating;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class PerformanceRatingService : IPerformanceRatingService
{
    private readonly IPerformanceRatingRepository _performanceRatingRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public PerformanceRatingService(IPerformanceRatingRepository performanceRatingRepository,
        ISubjectRepository subjectRepository,
        IMapper mapper)
    {
        _performanceRatingRepository = performanceRatingRepository;
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetPerformanceRatingDto>> GetPerformanceRatingByIdAsync(int performanceRatingId)
    {
        var response = new BaseResponse<GetPerformanceRatingDto>();

        var homework = await _performanceRatingRepository.GetByIdAsync(performanceRatingId);

        if (homework == null)
        {
            response.IsError = true;
            response.Description = $"Оценка по предмету с id - {performanceRatingId} не найдена";
            return response;
        }

        var mapPerformanceRating = _mapper.Map<GetPerformanceRatingDto>(homework);

        response.Data = mapPerformanceRating;
        return response;
    }

    public async Task<BaseResponse<List<GetPerformanceRatingDto>>> GetPerformanceRatingByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetPerformanceRatingDto>>();

        var homework = _performanceRatingRepository.Get(_ => true, request);

        if (homework == null || !homework.Any())
        {
            return response;
        }
        
        var mapPerformanceRating = _mapper.Map<List<GetPerformanceRatingDto>>(homework);

        response.Data = mapPerformanceRating;
        return response;
    }

    public async Task<BaseResponse<GetPerformanceRatingDto>> CreatePerformanceRatingAsync(CreatePerformanceRatingDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetPerformanceRatingDto>> UpdatePerformanceRatingByIdAsync(int performanceRatingId, UpdatePerformanceRatingDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<string>> DeletePerformanceRatingByIdAsync(int performanceRatingId)
    {
        var response = new BaseResponse<string>();

        var homework = await _performanceRatingRepository.GetByIdAsync(performanceRatingId);

        if (homework == null)
        {
            response.IsError = true;
            response.Description = $"Оценка по предмету с id - {performanceRatingId} не найдена";
            return response;
        }

        await _performanceRatingRepository.DeleteAsync(homework);

        response.Data = "Удалено";
        return response;
    }
}