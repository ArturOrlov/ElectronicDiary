using AutoMapper;
using ElectronicDiary.Dto.UserRole;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public UserRoleService(IUserRoleRepository userRoleRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IMapper mapper)
    {
        _userRoleRepository = userRoleRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
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
        var response = new BaseResponse<GetUserRoleDto>();

        var checkUserRole = _userRoleRepository.Get(ur => ur.RoleId == request.RoleId && ur.UserId == request.UserId).FirstOrDefault();

        if (checkUserRole != null)
        {
            response.IsError = true;
            response.Description = $"Связь пользователь-роль с пользователем с id - {request.UserId} и роли с id - {request.RoleId} уже существует";
            return response;
        }

        var user = await _userRepository.GetByIdAsync(request.UserId);
        
        if (user == null)
        {
            response.IsError = true;
            response.Description = $"Пользователя с id - {request.UserId} не найден";
            return response;
        }
        
        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        
        if (role == null)
        {
            response.IsError = true;
            response.Description = $"Роль с id - {request.RoleId} не найдена";
            return response;
        }
        
        var userRole = _mapper.Map<UserRole>(request);

        await _userRoleRepository.CreateAsync(userRole);
        
        var mapUserRole = _mapper.Map<GetUserRoleDto>(userRole);
        
        response.Data = mapUserRole;
        return response;
    }

    public async Task<BaseResponse<GetUserRoleDto>> UpdateUserRoleByIdAsync(int userRoleId, UpdateUserRoleDto request)
    {
        var response = new BaseResponse<GetUserRoleDto>();
        
        var userRole = await _userRoleRepository.GetByIdAsync(userRoleId);

        if (userRole == null)
        {
            response.IsError = true;
            response.Description = $"Связь пользователь-роль с id - {userRoleId} не найдена";
            return response;
        }

        var checkUserRole = _userRoleRepository.Get(ur => ur.RoleId == request.RoleId && ur.UserId == request.UserId).FirstOrDefault();

        if (checkUserRole != null)
        {
            response.IsError = true;
            response.Description = $"Связь пользователь-роль с пользователем с id - {request.UserId} и роли с id - {request.RoleId} уже существует";
            return response;
        }

        if (request.UserId.HasValue && request.UserId != 0)
        {
            var user = await _userRepository.GetByIdAsync((int)request.UserId);
        
            if (user == null)
            {
                response.IsError = true;
                response.Description = $"Пользователя с id - {request.UserId} не найден";
                return response;
            }

            userRole.UserId = (int)request.UserId;
        }
        
        if (request.RoleId.HasValue && request.RoleId != 0)
        {
            var role = await _roleRepository.GetByIdAsync((int)request.RoleId);
        
            if (role == null)
            {
                response.IsError = true;
                response.Description = $"Роль с id - {request.RoleId} не найдена";
                return response;
            }
            
            userRole.RoleId = (int)request.RoleId;
        }

        userRole.UpdatedAt = DateTimeOffset.Now;
        await _userRoleRepository.UpdateAsync(userRole);
        
        var mapUserRole = _mapper.Map<GetUserRoleDto>(userRole);
        
        response.Data = mapUserRole;
        return response;
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