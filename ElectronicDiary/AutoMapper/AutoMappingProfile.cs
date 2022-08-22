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
        CreateMap<GetTimetableDto, Timetable>();
        CreateMap<CreateTimetableDto, Timetable>();
        
        // Homework
        CreateMap<GetHomeworkDto, Homework>();
        CreateMap<CreateHomeworkDto, Homework>();
        
        // PerformanceRating
        CreateMap<GetPerformanceRatingDto, PerformanceRating>();
        CreateMap<CreatePerformanceRatingDto, PerformanceRating>();
        
        // SchoolClass
        CreateMap<GetSchoolClassDto, SchoolClass>();
        CreateMap<CreateSchoolClassDto, SchoolClass>();
        
        // Subject
        CreateMap<GetSubjectDto, Subject>();
        CreateMap<CreateSubjectDto, Subject>();
        
        // User
        CreateMap<GetUserDto, User>();
        CreateMap<CreateUserDto, User>();
        
        // Role
        CreateMap<GetRoleDto, Role>();
        CreateMap<CreateRoleDto, Role>();
    }
}