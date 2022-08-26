using AutoMapper;
using ElectronicDiary.Context;
using ElectronicDiary.Dto.User;
using ElectronicDiary.Dto.UserInfo;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDiary.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ElectronicDiaryDbContext _context;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager,
        RoleManager<Role> roleManager, 
        ElectronicDiaryDbContext context, 
        IUserInfoRepository userInfoRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _userInfoRepository = userInfoRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<User>> GetByLoginAsync(string login, string password)
    {
        var response = new BaseResponse<User>();

        var user = await _userManager.FindByNameAsync(login);

        if (user == null)
        {
            response.IsError = true;
            response.Description = "Невыерный логин или пароль";
            return response;
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);

        if (isPasswordCorrect == false)
        {
            response.IsError = true;
            response.Description = "Невыерный логин или пароль";
            return response;
        }

        response.Data = user;
        return response;
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

    public async Task<BaseResponse<List<GetUserDto>>> GetByPaginationAsync(UserFilter request)
    {
        var response = new BaseResponse<List<GetUserDto>>();

        var users = await (from u in _userManager.Users
            join ur in _context.UserRole on u.Id equals ur.UserId
            join ui in _context.UserInfo on u.Id equals ui.UserId into uid
            from ui in uid.DefaultIfEmpty()
            where request.RoleId == 0 || ur.RoleId == request.RoleId
            select new GetUserDto
            {
                Id = u.Id,
                RoleId = ur.RoleId,
                Email = u.Email,
                UserName = u.UserName,
                PhoneNumber = u.PhoneNumber,
                UserInfo = _mapper.Map<GetUserInfoDto>(ui)
            }).Skip(request.Skip).Take(request.Take).ToListAsync();

        response.Data = users;
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

        // await _userInfoRepository.CreateAsync(new UserInfo()
        // {
        //     UserId = user.Id
        // });

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

        user.UpdatedAt = DateTimeOffset.Now;
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