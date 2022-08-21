using ElectronicDiary.Dto.User;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;

namespace ElectronicDiary.Services;

public class UserService : IUserService
{
    private readonly UserManager<Entities.DbModels.User> _userManager;

    public UserService(UserManager<Entities.DbModels.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<BaseResponse<GetUserDto>> GetUserByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<List<GetUserDto>>> GetUserByPaginationAsync(BasePagination request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetUserDto>> CreateUserAsync(CreateUserDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetUserDto>> UpdateUserByIdAsync(int userId, UpdateUserDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetUserDto>> DeleteUserByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
}