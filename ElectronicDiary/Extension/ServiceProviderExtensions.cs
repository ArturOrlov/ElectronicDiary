using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;
using ElectronicDiary.Repositories;
using ElectronicDiary.Services;
using Microsoft.Extensions.Caching.Memory;

namespace ElectronicDiary.Extension;

public static class ServiceProviderExtensions
{
    /// <summary>
    /// Зарегестрировать сервисы.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<Bootstrap.Bootstrap>();
        services.AddSingleton<IMemoryCache, MemoryCache>();
        
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IUserRoleService, UserRoleService>();
        services.AddTransient<IUserInfoService, UserInfoService>();
        services.AddTransient<IUserClassService, UserClassService>();

        services.AddTransient<ICabinetService, CabinetService>();
        services.AddTransient<ISubjectService, SubjectService>();
        services.AddTransient<IHomeworkService, HomeworkService>();
        services.AddTransient<ITimetableService, TimetableService>();
        services.AddTransient<ISchoolClassService, SchoolClassService>();
        services.AddTransient<IPerformanceRatingService, PerformanceRatingService>();
    }
    
    /// <summary>
    /// Зарегестрировать репозитории.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddCustomRepository(this IServiceCollection services)
    {
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserRoleRepository, UserRoleRepository>();
        services.AddTransient<IUserInfoRepository, UserInfoRepository>();
        services.AddTransient<IUserClassRepository, UserClassRepository>();

        services.AddTransient<ICabinetRepository, CabinetRepository>();
        services.AddTransient<ISubjectRepository, SubjectRepository>();
        services.AddTransient<IHomeworkRepository, HomeworkRepository>();
        services.AddTransient<ITimetableRepository, TimetableRepository>();
        services.AddTransient<ISchoolClassRepository, SchoolClassRepository>();
        services.AddTransient<IPerformanceRatingRepository, PerformanceRatingRepository>();
    }
}