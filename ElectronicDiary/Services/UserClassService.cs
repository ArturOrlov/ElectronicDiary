using AutoMapper;
using ElectronicDiary.Dto.UserClass;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class UserClassService : IUserClassService
{
    private readonly IUserClassRepository _userClassRepository;
    private readonly IMapper _mapper;

    public UserClassService(IUserClassRepository userClassRepository, 
        IMapper mapper)
    {
        _userClassRepository = userClassRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetUserClassDto>> GetUserClassByIdAsync(int userClassId)
    {
        var response = new BaseResponse<GetUserClassDto>();

        var timetable = await _userClassRepository.GetByIdAsync(userClassId);

        if (timetable == null)
        {
            response.Description = $"Связь пользователь - класс с id - {userClassId} не найдена";
            response.IsError = true;
            return response;
        }

        var mapTimetable = _mapper.Map<GetUserClassDto>(timetable);

        response.Data = mapTimetable;
        return response;
    }

    public async Task<BaseResponse<List<GetUserClassDto>>> GetUserClassByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetUserClassDto>>();

        var timetables = _userClassRepository.Get(_ => true, request);

        if (timetables == null || !timetables.Any())
        {
            return response;
        }

        var mapSubject = _mapper.Map<List<GetUserClassDto>>(timetables);

        response.Data = mapSubject;
        return response;
    }

    public async Task<BaseResponse<GetUserClassDto>> CreateUserClassAsync(CreateUserClassDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetUserClassDto>> UpdateUserClassByIdAsync(int userClassId, UpdateUserClassDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<string>> DeleteUserClassByIdAsync(int userClassId)
    {
        var response = new BaseResponse<string>();

        var subject = await _userClassRepository.GetByIdAsync(userClassId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Связь пользователь - класс с id - {userClassId} не найдена";
            return response;
        }

        await _userClassRepository.DeleteAsync(subject);

        response.Data = "Удалено";
        return response;
    }
}