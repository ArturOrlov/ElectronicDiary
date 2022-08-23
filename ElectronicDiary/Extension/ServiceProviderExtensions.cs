using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;
using ElectronicDiary.Repositories;
using ElectronicDiary.Services;

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
        
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ISchoolClassService, SchoolClassService>();
        services.AddTransient<ISubjectService, SubjectService>();
        services.AddTransient<ITimetableService, TimetableService>();
        services.AddTransient<IPerformanceRatingService, PerformanceRatingService>();
        services.AddTransient<IHomeworkService, HomeworkService>();
    }
    
    /// <summary>
    /// Зарегестрировать репозитории.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddCustomRepository(this IServiceCollection services)
    {
        services.AddTransient<ISchoolClassRepository, SchoolClassRepository>();
        services.AddTransient<ISubjectRepository, SubjectRepository>();
        services.AddTransient<ITimetableRepository, TimetableRepository>();
        services.AddTransient<IPerformanceRatingRepository, PerformanceRatingRepository>();
        services.AddTransient<IHomeworkRepository, HomeworkRepository>();
    }
}