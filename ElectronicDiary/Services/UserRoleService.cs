using AutoMapper;
using ElectronicDiary.Dto.UserRole;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IMapper _mapper;

    public UserRoleService(IUserRoleRepository userRoleRepository,
        IMapper mapper)
    {
        _userRoleRepository = userRoleRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetUserRoleDto>> GetUserRoleByIdAsync(int userRoleId)
    {
        var response = new BaseResponse<GetUserRoleDto>();

        var userRole = await _userRoleRepository.GetByIdAsync(userRoleId);

        if (userRole == null)
        {
            response.IsError = true;
            response.Description = $"Связь пользователь-роль с id - {userRoleId} не найдена";
            return response;
        }

        var mapUserRole = _mapper.Map<GetUserRoleDto>(userRole);

        response.Data = mapUserRole;
        return response;
    }

    public async Task<BaseResponse<GetUserRoleDto>> GetUserRoleByUserIdAsync(int userId)
    {
        var response = new BaseResponse<GetUserRoleDto>();

        var userRole = _userRoleRepository.Get(ur => ur.UserId == userId).FirstOrDefault();

        if (userRole == null)
        {
            response.IsError = true;
            response.Description = $"Связь пользователь-роль с по id пользователь - {userId} не найдена";
            return response;
        }

        var mapUserRole = _mapper.Map<GetUserRoleDto>(userRole);

        response.Data = mapUserRole;
        return response;
    }

    public async Task<BaseResponse<List<GetUserRoleDto>>> GetUserRoleByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetUserRoleDto>>();

        var userRoles = _userRoleRepository.Get(_ => true, request);

        if (userRoles == null || !userRoles.Any())
        {
            return response;
        }

        var mapUserRole = _mapper.Map<List<GetUserRoleDto>>(userRoles);

        response.Data = mapUserRole;
        return response;
    }

    public async Task<BaseResponse<GetUserRoleDto>> CreateUserRoleAsync(CreateUserRoleDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetUserRoleDto>> UpdateUserRoleByIdAsync(int userRoleId, UpdateUserRoleDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<string>> DeleteUserRoleByIdAsync(int userRoleId)
    {
        var response = new BaseResponse<string>();

        var userRole = await _userRoleRepository.GetByIdAsync(userRoleId);

        if (userRole == null)
        {
            response.IsError = true;
            response.Description = $"Связь пользователь-роль id - {userRoleId} не найдена";
            return response;
        }

        await _userRoleRepository.DeleteAsync(userRole);

        response.Data = "Удалено";
        return response;
    }
}