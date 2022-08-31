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
            response.Description = "Неверный логин или пароль";
            return response;
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);

        if (isPasswordCorrect == false)
        {
            response.IsError = true;
            response.Description = "Неверный логин или пароль";
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

        var userInfo = await _userInfoRepository.GetByUserId(userId);

        if (userInfo != null)
        {
            mapUser.UserInfo = _mapper.Map<GetUserInfoDto>(userInfo);
        }

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
            response.Description = $"Пользователь с почтой - {request.Email} уже существует";
            return response;
        }

        var checkUserName = await _userManager.FindByEmailAsync(request.UserName);

        if (checkUserName != null)
        {
            response.IsError = true;
            response.Description = $"Пользователь с именем профиля - {request.UserName} уже существует";
            return response;
        }

        if (request.Password != request.ConfirmPassword)
        {
            response.IsError = true;
            response.Description = $"Пользователь пароль и подтверждённый пароль не совпадают";
            return response;
        }

        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

        if (role == null)
        {
            response.IsError = true;
            response.Description = $"Роль с id - {request.RoleId} не найдена";
            return response;
        }

        var user = _mapper.Map<User>(request);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            response.IsError = true;
            response.Description = $"Ошибка создания пользователя";
            return response;
        }
        
        await _userManager.AddToRoleAsync(user, role.Name);

        if (request.UserInfo != null)
        {
            // var isValidInfo = await ValidateUserInfo(request.UserInfo);
            //
            // if (isValidInfo == false)
            // {
            //     response.Description = $"Обнаружены ошибки в переданных данных пользовательской информации. Проверьте данные и повторите попытку";
            // }

            var userInfo = _mapper.Map<UserInfo>(request.UserInfo);

            userInfo.UserId = user.Id;
            
            await _userInfoRepository.CreateAsync(userInfo);
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
                response.Description = $"Пользователь с почтой - {request.Email} уже существует";
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
        
        if (request.UserInfo != null)
        {
            // var isValidInfo = await ValidateUserInfo(request.UserInfo);
            //
            // if (isValidInfo == false)
            // {
            //     response.Description = $"Ошибки в переданных данных пользовательской информации. Проверьте данные и повторите попытку добавить данные";
            // }

            var userInfo = _mapper.Map<UserInfo>(request.UserInfo);

            userInfo.UserId = user.Id;
            
            await _userInfoRepository.CreateAsync(userInfo);
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
    
    // // todo подумать над тем что бы сделать модели create и update более абстрактными и объединить их
    // private async Task<bool> ValidateUserInfo(CreateUserInfoDto userInfo)
    // {
    //     if (userInfo.ChildrenUserId != null && userInfo.IsParent && userInfo.ChildrenUserId.Any())
    //     {
    //         var student = await _userManager.FindByIdAsync(userInfo.ChildrenUserId);
    //
    //         if (student == null)
    //         {
    //             return false;
    //         }
    //     }
    //
    //     return true;
    // }
    //
    // private async Task<bool> ValidateUserInfo(UpdateUserInfoDto userInfo)
    // {
    //     if (userInfo.ChildrenUserId != null && userInfo.IsParent && userInfo.ChildrenUserId.Any())
    //     {
    //         var student = await _userManager.FindByIdAsync(userInfo.ChildrenUserId);
    //
    //         if (student == null)
    //         {
    //             return false;
    //         }
    //     }
    //
    //     return true;
    // }
}