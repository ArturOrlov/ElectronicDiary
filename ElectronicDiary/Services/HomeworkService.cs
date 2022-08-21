using ElectronicDiary.Dto.Homework;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class HomeworkService : IHomeworkService
{
    private readonly IHomeworkRepository _homeworkRepository;

    public HomeworkService(IHomeworkRepository homeworkRepository)
    {
        _homeworkRepository = homeworkRepository;
    }

    public async Task<BaseResponse<GetHomeworkDto>> GetHomeworkByIdAsync(int homeworkId)
    {
        var response = new BaseResponse<GetHomeworkDto>();

        var a = await _homeworkRepository.GetByIdAsync(homeworkId);

        if (a == null)
        {
            response.IsError = true;
            response.Description = "asdad";
            return response;
        }

        var s = new GetHomeworkDto()
        {

        };

        response.Data = s;
        return response;
    }

    public async Task<BaseResponse<List<GetHomeworkDto>>> GetHomeworkByPaginationAsync(BasePagination request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetHomeworkDto>> CreateHomeworkAsync(CreateHomeworkDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetHomeworkDto>> UpdateHomeworkByIdAsync(int homeworkId, UpdateHomeworkDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetHomeworkDto>> DeleteHomeworkByIdAsync(int homeworkId)
    {
        throw new NotImplementedException();
    }
}