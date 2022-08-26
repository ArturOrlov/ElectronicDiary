using AutoMapper;
using ElectronicDiary.Dto.UserInfo;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class UserInfoService : IUserInfoService
{
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserInfoService(IUserInfoRepository userInfoRepository,
        IMapper mapper, IUserRepository userRepository)
    {
        _userInfoRepository = userInfoRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<GetUserInfoDto>> GetByIdAsync(int userId)
    {
        var response = new BaseResponse<GetUserInfoDto>();

        var userInfo = _userInfoRepository.Get(ui => ui.UserId == userId).FirstOrDefault();

        if (userInfo == null)
        {
            response.IsError = true;
            response.Description = "Невыерный логин или пароль";
            return response;
        }

        var mapUserInfo = _mapper.Map<GetUserInfoDto>(userInfo);

        response.Data = mapUserInfo;
        return response;
    }

    public async Task<BaseResponse<GetUserInfoDto>> CreateAsync(int userId, CreateUserInfoDto request)
    {
        var response = new BaseResponse<GetUserInfoDto>();

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            response.IsError = true;
            response.Description = "Невыерный логин или пароль";
            return response;
        }

        var userInfo = _mapper.Map<UserInfo>(request);

        await _userInfoRepository.CreateAsync(userInfo);

        var mapUserInfo = _mapper.Map<GetUserInfoDto>(userInfo);

        response.Data = mapUserInfo;
        return response;
    }

    public async Task<BaseResponse<GetUserInfoDto>> UpdateByIdAsync(int userId, UpdateUserInfoDto request)
    {
        var response = new BaseResponse<GetUserInfoDto>();

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            response.IsError = true;
            response.Description = "Невыерный логин или пароль";
            return response;
        }
        
        var userInfo = _userInfoRepository.Get(ui => ui.UserId == userId).FirstOrDefault();

        if (userInfo == null)
        {
            var createUserInfo = _mapper.Map<CreateUserInfoDto>(request);
            var result = await CreateAsync(userId, createUserInfo);

            var mapCreateUserInfo = _mapper.Map<GetUserInfoDto>(result.Data);
            
            response.Data = mapCreateUserInfo;
            return response;
        }

        userInfo.UpdatedAt = DateTimeOffset.Now;
        await _userInfoRepository.UpdateAsync(userInfo);

        var mapUserInfo = _mapper.Map<GetUserInfoDto>(userInfo);

        response.Data = mapUserInfo;
        return response;
    }
}