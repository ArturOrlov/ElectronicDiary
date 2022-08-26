using AutoMapper;
using ElectronicDiary.Dto.Cabinet;
using ElectronicDiary.Dto.Homework;
using ElectronicDiary.Dto.PerformanceRating;
using ElectronicDiary.Dto.Role;
using ElectronicDiary.Dto.SchoolClass;
using ElectronicDiary.Dto.Subject;
using ElectronicDiary.Dto.Timetable;
using ElectronicDiary.Dto.User;
using ElectronicDiary.Dto.UserClass;
using ElectronicDiary.Dto.UserInfo;
using ElectronicDiary.Dto.UserRole;
using ElectronicDiary.Entities.DbModels;

namespace ElectronicDiary.AutoMapper;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        // Timetable
        CreateMap<Timetable, GetTimetableDto>().ReverseMap();
        CreateMap<Timetable, CreateTimetableDto>().ReverseMap();

        // Homework
        CreateMap<Homework, GetHomeworkDto>().ReverseMap();
        CreateMap<Homework, CreateHomeworkDto>().ReverseMap();

        // PerformanceRating
        CreateMap<PerformanceRating, GetPerformanceRatingDto>().ReverseMap();
        CreateMap<PerformanceRating, CreatePerformanceRatingDto>().ReverseMap();

        // SchoolClass
        CreateMap<SchoolClass, GetSchoolClassDto>().ReverseMap();
        CreateMap<SchoolClass, CreateSchoolClassDto>().ReverseMap();

        // Subject
        CreateMap<Subject, GetSubjectDto>().ReverseMap();
        CreateMap<Subject, CreateSubjectDto>().ReverseMap();
        
        // Cabinet
        CreateMap<Cabinet, GetCabinetDto>().ReverseMap();
        CreateMap<Cabinet, CreateCabinetDto>().ReverseMap();

        // User
        CreateMap<User, GetUserDto>().ReverseMap();
        CreateMap<User, CreateUserDto>().ReverseMap();

        // UserInfo
        CreateMap<UserInfo, GetUserInfoDto>().ReverseMap();
        CreateMap<UserInfo, CreateUserInfoDto>().ReverseMap();

        // UserClass
        CreateMap<UserClass, GetUserClassDto>().ReverseMap();
        CreateMap<UserClass, CreateUserClassDto>().ReverseMap();

        // UserRole
        CreateMap<UserRole, GetUserRoleDto>().ReverseMap();
        CreateMap<UserRole, CreateUserRoleDto>().ReverseMap();

        // Role
        CreateMap<Role, GetRoleDto>().ReverseMap();
        CreateMap<Role, CreateRoleDto>().ReverseMap();
    }
}