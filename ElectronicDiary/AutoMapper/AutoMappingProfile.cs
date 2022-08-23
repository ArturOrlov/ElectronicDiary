using AutoMapper;
using ElectronicDiary.Dto.Homework;
using ElectronicDiary.Dto.PerformanceRating;
using ElectronicDiary.Dto.Role;
using ElectronicDiary.Dto.SchoolClass;
using ElectronicDiary.Dto.Subject;
using ElectronicDiary.Dto.Timetable;
using ElectronicDiary.Dto.User;
using ElectronicDiary.Entities.DbModels;

namespace ElectronicDiary.AutoMapper;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {			
        // Timetable
        CreateMap<Timetable, GetTimetableDto>();
        CreateMap<GetTimetableDto, Timetable>();
        CreateMap<Timetable, CreateTimetableDto>();
        CreateMap<CreateTimetableDto, Timetable>();
        
        // Homework
        CreateMap<Homework, GetHomeworkDto>();
        CreateMap<GetHomeworkDto, Homework>();
        CreateMap<Homework, CreateHomeworkDto>();
        CreateMap<CreateHomeworkDto, Homework>();
        
        // PerformanceRating
        CreateMap<PerformanceRating, GetPerformanceRatingDto>();
        CreateMap<GetPerformanceRatingDto, PerformanceRating>();
        CreateMap<PerformanceRating, CreatePerformanceRatingDto>();
        CreateMap<CreatePerformanceRatingDto, PerformanceRating>();
        
        // SchoolClass
        CreateMap<SchoolClass, GetSchoolClassDto>();
        CreateMap<GetSchoolClassDto, SchoolClass>();
        CreateMap<SchoolClass, CreateSchoolClassDto>();
        CreateMap<CreateSchoolClassDto, SchoolClass>();
        
        // Subject
        CreateMap<Subject, GetSubjectDto>();
        CreateMap<GetSubjectDto, Subject>();
        CreateMap<Subject, CreateSubjectDto>();
        CreateMap<CreateSubjectDto, Subject>();
        
        // User
        CreateMap<User, GetUserDto>();
        CreateMap<GetUserDto, User>();
        CreateMap<User, CreateUserDto>();
        CreateMap<CreateUserDto, User>();
        
        // Role
        CreateMap<Role, GetRoleDto>();
        CreateMap<GetRoleDto, Role>();
        CreateMap<Role, CreateRoleDto>();
        CreateMap<CreateRoleDto, Role>();
    }
}