using AutoMapper;
using ElectronicDiary.Dto.User;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;

namespace ElectronicDiary.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<BaseResponse<GetUserDto>> GetByIdAsync(int userId)
    {
        var response = new BaseResponse<GetUserDto>();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            response.IsError = true;
            response.Description = $"Пользователь с id - {userId} не найден";
            return response;
        }

        var mapUser = _mapper.Map<GetUserDto>(user);

        response.Data = mapUser;
        return response;
    }

    public async Task<BaseResponse<List<GetUserDto>>> GetByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetUserDto>>();

        var users = _userManager.Users.ToList();

        if (!users.Any())
        {
            return response;
        }

        users = users.Skip(request.Skip).Take(request.Take).ToList();

        var mapHomework = _mapper.Map<List<GetUserDto>>(users);

        response.Data = mapHomework;
        return response;
    }

    public async Task<BaseResponse<GetUserDto>> CreateAsync(CreateUserDto request)
    {
        var response = new BaseResponse<GetUserDto>();

        var checkEmail = await _userManager.FindByEmailAsync(request.Email);

        if (checkEmail != null)
        {
            response.IsError = true;
            response.Description = $"Пользователь с id - {request.Email} ужесуществует";
            return response;
        }

        var checkUserName = await _userManager.FindByEmailAsync(request.UserName);

        if (checkUserName != null)
        {
            response.IsError = true;
            response.Description = $"Пользователь с id - {request.UserName} ужесуществует";
            return response;
        }

        if (request.Password != request.ConfirmPassword)
        {
            response.IsError = true;
            response.Description = $"Пользователь с id - {request.UserName} ужесуществует";
            return response;
        }

        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

        if (role == null)
        {
            response.IsError = true;
            response.Description = $"Пользователь с id - {request.UserName} ужесуществует";
            return response;
        }

        var user = _mapper.Map<User>(request);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role.Name);
        }

        var mapUser = _mapper.Map<GetUserDto>(user);

        response.Data = mapUser;
        return response;
    }

    public async Task<BaseResponse<GetUserDto>> UpdateByIdAsync(int userId, UpdateUserDto request)
    {
        var response = new BaseResponse<GetUserDto>();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (!string.IsNullOrEmpty(request.Email))
        {
            var checkEmail = await _userManager.FindByEmailAsync(request.Email);

            if (checkEmail != null)
            {
                response.IsError = true;
                response.Description = $"Пользователь с id - {request.Email} ужесуществует";
                return response;
            }

            user.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.PhoneNumber))
        {
            user.PhoneNumber = request.PhoneNumber;
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            response.IsError = true;
            response.Description = result.Errors.ToString();
            return response;
        }

        var mapUser = _mapper.Map<GetUserDto>(user);

        response.Data = mapUser;
        return response;
    }

    public async Task<BaseResponse<string>> DeleteByIdAsync(int userId)
    {
        var response = new BaseResponse<string>();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            response.IsError = true;
            response.Description = $"Пользователь с id - {userId} не найден";
            return response;
        }

        await _userManager.DeleteAsync(user);

        response.Data = "Удалено";
        return response;
    }
}