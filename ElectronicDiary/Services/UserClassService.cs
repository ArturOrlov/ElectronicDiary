using AutoMapper;
using ElectronicDiary.Dto.UserClass;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class UserClassService : IUserClassService
{
    private readonly IUserClassRepository _userClassRepository;
    private readonly ISchoolClassRepository _schoolClassRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserClassService(IUserClassRepository userClassRepository,
        ISchoolClassRepository schoolClassRepository, 
        IUserRepository userRepository, 
        IMapper mapper)
    {
        _userClassRepository = userClassRepository;
        _schoolClassRepository = schoolClassRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<GetUserClassDto>> GetUserClassByIdAsync(int userClassId)
    {
        var response = new BaseResponse<GetUserClassDto>();

        var userClass = await _userClassRepository.GetByIdAsync(userClassId);

        if (userClass == null)
        {
            response.Description = $"Связь пользователь-класс с id - {userClassId} не найдена";
            response.IsError = true;
            return response;
        }

        var mapUserClass = _mapper.Map<GetUserClassDto>(userClass);

        response.Data = mapUserClass;
        return response;
    }

    public async Task<BaseResponse<List<GetUserClassDto>>> GetUserClassByPaginationAsync(BasePagination request)
    {
        var response = new BaseResponse<List<GetUserClassDto>>();

        var userClasses = _userClassRepository.Get(_ => true, request);

        if (userClasses == null || !userClasses.Any())
        {
            return response;
        }

        var mapUserClass = _mapper.Map<List<GetUserClassDto>>(userClasses);

        response.Data = mapUserClass;
        return response;
    }

    public async Task<BaseResponse<GetUserClassDto>> CreateUserClassAsync(CreateUserClassDto request)
    {
        var response = new BaseResponse<GetUserClassDto>();
        
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            response.Description = $"Пользователь с id - {request.UserId} не найдена";
            response.IsError = true;
            return response;
        }
        
        var schoolClass = await _schoolClassRepository.GetByIdAsync(request.SchoolClassId);

        if (schoolClass == null)
        {
            response.Description = $"Класс с id - {request.SchoolClassId} не найдена";
            response.IsError = true;
            return response;
        }
        
        // todo сделать проверку на роль. Ученики и родители не могут быть классными
        if (request.IsClassroomTeacher == true)
        {
            
        }
        
        var userClass = _mapper.Map<UserClass>(request);

        await _userClassRepository.CreateAsync(userClass);
        
        var mapUserClass = _mapper.Map<GetUserClassDto>(userClass);
        
        response.Data = mapUserClass;
        return response;
    }

    public async Task<BaseResponse<GetUserClassDto>> UpdateUserClassByIdAsync(int userClassId, UpdateUserClassDto request)
    {
        var response = new BaseResponse<GetUserClassDto>();
        
        var userClass = await _userClassRepository.GetByIdAsync(userClassId);

        if (userClass == null)
        {
            response.Description = $"Связь пользователь-класс с id - {userClassId} не найдена";
            response.IsError = true;
            return response;
        }

        if (request.UserId.HasValue && request.UserId != 0)
        {
            var user = await _userRepository.GetByIdAsync((int)request.UserId);

            if (user == null)
            {
                response.Description = $"Пользователь с id - {request.UserId} не найдена";
                response.IsError = true;
                return response;
            }

            userClass.UserId = (int)request.UserId;
        }

        if (request.SchoolClassId.HasValue && request.SchoolClassId != 0)
        {
            var schoolClass = await _schoolClassRepository.GetByIdAsync((int)request.SchoolClassId);

            if (schoolClass == null)
            {
                response.Description = $"Класс с id - {request.SchoolClassId} не найдена";
                response.IsError = true;
                return response;
            }
            
            userClass.SchoolClassId = (int)request.SchoolClassId;
        }

        if (request.IsClassroomTeacher.HasValue)
        {
            // todo сделать проверку на роль. Ученики и родители не могут быть классными
            
            userClass.IsClassroomTeacher = (bool)request.IsClassroomTeacher;
        }

        userClass.UpdatedAt = DateTimeOffset.Now;
        await _userClassRepository.UpdateAsync(userClass);
        
        var mapUserClass = _mapper.Map<GetUserClassDto>(userClass);
        
        response.Data = mapUserClass;
        return response;
    }

    public async Task<BaseResponse<string>> DeleteUserClassByIdAsync(int userClassId)
    {
        var response = new BaseResponse<string>();

        var subject = await _userClassRepository.GetByIdAsync(userClassId);

        if (subject == null)
        {
            response.IsError = true;
            response.Description = $"Связь пользователь-класс с id - {userClassId} не найдена";
            return response;
        }

        await _userClassRepository.DeleteAsync(subject);

        response.Data = "Удалено";
        return response;
    }
}