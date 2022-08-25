using AutoMapper;
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

    // public AutoMappingProfile()
    // {
    //     // Timetable
    //     CreateMap<Timetable, GetTimetableDto>();
    //     CreateMap<GetTimetableDto, Timetable>();
    //     CreateMap<Timetable, CreateTimetableDto>();
    //     CreateMap<CreateTimetableDto, Timetable>();
    //
    //     // Homework
    //     CreateMap<Homework, GetHomeworkDto>();
    //     CreateMap<GetHomeworkDto, Homework>();
    //     CreateMap<Homework, CreateHomeworkDto>();
    //     CreateMap<CreateHomeworkDto, Homework>();
    //
    //     // PerformanceRating
    //     CreateMap<PerformanceRating, GetPerformanceRatingDto>();
    //     CreateMap<GetPerformanceRatingDto, PerformanceRating>();
    //     CreateMap<PerformanceRating, CreatePerformanceRatingDto>();
    //     CreateMap<CreatePerformanceRatingDto, PerformanceRating>();
    //
    //     // SchoolClass
    //     CreateMap<SchoolClass, GetSchoolClassDto>();
    //     CreateMap<GetSchoolClassDto, SchoolClass>();
    //     CreateMap<SchoolClass, CreateSchoolClassDto>();
    //     CreateMap<CreateSchoolClassDto, SchoolClass>();
    //
    //     // Subject
    //     CreateMap<Subject, GetSubjectDto>();
    //     CreateMap<GetSubjectDto, Subject>();
    //     CreateMap<Subject, CreateSubjectDto>();
    //     CreateMap<CreateSubjectDto, Subject>();
    //
    //     // User
    //     CreateMap<User, GetUserDto>();
    //     CreateMap<GetUserDto, User>();
    //     CreateMap<User, CreateUserDto>();
    //     CreateMap<CreateUserDto, User>();
    //
    //     // UserInfo
    //     CreateMap<UserInfo, GetUserInfoDto>();
    //     CreateMap<GetUserInfoDto, UserInfo>();
    //     CreateMap<UserInfo, CreateUserInfoDto>();
    //     CreateMap<CreateUserInfoDto, UserInfo>();
    //
    //     // UserClass
    //     CreateMap<UserClass, GetUserClassDto>();
    //     CreateMap<GetUserClassDto, UserClass>();
    //     CreateMap<UserClass, CreateUserClassDto>();
    //     CreateMap<CreateUserClassDto, UserClass>();
    //
    //     // UserRole
    //     CreateMap<UserRole, GetUserRoleDto>();
    //     CreateMap<GetUserRoleDto, UserRole>();
    //     CreateMap<UserRole, CreateUserRoleDto>();
    //     CreateMap<CreateUserRoleDto, UserRole>();
    //
    //     // Role
    //     CreateMap<Role, GetRoleDto>();
    //     CreateMap<GetRoleDto, Role>();
    //     CreateMap<Role, CreateRoleDto>();
    //     CreateMap<CreateRoleDto, Role>();
    //
    //     
    //     
    //     
    //     
    //     // Report !!!
    //     CreateMap<Role, GetPerformanceRatingReportDto>();
    //     CreateMap<GetPerformanceRatingReportDto, Role>();
    //     CreateMap<Role, GetSchoolClassReportDto>();
    //     CreateMap<GetSchoolClassReportDto, Role>();
    //     CreateMap<Role, GetSubjectReportDto>();
    //     CreateMap<GetSubjectReportDto, Role>();
    //     
    //     CreateMap<Role, ResponsePerformanceRatingReportDto>();
    //     CreateMap<ResponsePerformanceRatingReportDto, Role>().ReverseMap();
    //     CreateMap<Role, GetSchoolClassReportDto>();
    //     CreateMap<GetSchoolClassReportDto, Role>();
    //     CreateMap<Role, GetSubjectReportDto>();
    //     CreateMap<GetSubjectReportDto, Role>();
    // }
}