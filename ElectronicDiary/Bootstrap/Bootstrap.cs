using ElectronicDiary.Constants;
using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using Microsoft.AspNetCore.Identity;

namespace ElectronicDiary.Bootstrap;

public class Bootstrap
{
    private readonly ElectronicDiaryDbContext _context;
    private readonly UserManager<User> _userManager;

    public Bootstrap(ElectronicDiaryDbContext context,
        UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task SeedDb()
    {
        await SeedRolesAsync();
        await SeedAdminAsync();
        await SeedSchoolClassAsync();
    }

    private async Task SeedRolesAsync()
    {
        if (_context.Role.Any())
        {
            return;
        }

        // var roles = new List<Role>()
        // {
        //     new()
        //     {
        //         Name = RoleConstant.RoleAdmin, 
        //         NormalizedName = RoleConstant.RoleAdmin.ToUpper(),
        //         ConcurrencyStamp = Convert.ToString(DateTimeOffset.Now.Ticks), 
        //         CreatedAt = DateTimeOffset.Now,
        //         UpdatedAt = DateTimeOffset.Now
        //     },
        //     new()
        //     {
        //         Name = RoleConstant.RoleHeadTeacher, 
        //         NormalizedName = RoleConstant.RoleHeadTeacher.ToUpper(),
        //         ConcurrencyStamp = Convert.ToString(DateTimeOffset.Now.Ticks), 
        //         CreatedAt = DateTimeOffset.Now,
        //         UpdatedAt = DateTimeOffset.Now
        //     },
        //     new()
        //     {
        //         Name = RoleConstant.RoleTeacher, 
        //         NormalizedName = RoleConstant.RoleTeacher.ToUpper(),
        //         ConcurrencyStamp = Convert.ToString(DateTimeOffset.Now.Ticks), 
        //         CreatedAt = DateTimeOffset.Now,
        //         UpdatedAt = DateTimeOffset.Now
        //     },
        //     new()
        //     {
        //         Name = RoleConstant.RoleStudent, 
        //         NormalizedName = RoleConstant.RoleStudent.ToUpper(),
        //         ConcurrencyStamp = Convert.ToString(DateTimeOffset.Now.Ticks), 
        //         CreatedAt = DateTimeOffset.Now,
        //         UpdatedAt = DateTimeOffset.Now
        //     },
        //     new()
        //     {
        //         Name = RoleConstant.RoleParent, 
        //         NormalizedName = RoleConstant.RoleParent.ToUpper(),
        //         ConcurrencyStamp = Convert.ToString(DateTimeOffset.Now.Ticks), 
        //         CreatedAt = DateTimeOffset.Now,
        //         UpdatedAt = DateTimeOffset.Now
        //     },
        // };

        var roles = new List<Role>()
        {
            RoleSeedHelper(RoleConstant.Admin),
            RoleSeedHelper(RoleConstant.HeadTeacher),
            RoleSeedHelper(RoleConstant.Teacher),
            RoleSeedHelper(RoleConstant.Student),
            RoleSeedHelper(RoleConstant.Parent)
        };

        await _context.AddRangeAsync(roles);
        await _context.SaveChangesAsync();
    }

    private async Task SeedAdminAsync()
    {
        if (_context.Users.Any(u => u.UserName == AdminDefaultSettings.UserName))
        {
            return;
        }

        var user = new User()
        {
            Email = AdminDefaultSettings.Email,
            NormalizedEmail = AdminDefaultSettings.Email.ToUpper(),
            UserName = AdminDefaultSettings.UserName,
            NormalizedUserName = AdminDefaultSettings.UserName.ToUpper(),
            PhoneNumber = "+111111111111",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        var result = await _userManager.CreateAsync(user, AdminDefaultSettings.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, RoleConstant.Admin);
        }
    }

    private async Task SeedSchoolClassAsync()
    {
        if (_context.SchoolClass.Any())
        {
            return;
        }

        var schoolClasses = new List<SchoolClass>()
        {
            new() { CreatedAt = ClassDateSeedHelper(2022), Symbol = "А" }, // 1 год/класс
            new() { CreatedAt = ClassDateSeedHelper(2021), Symbol = "Б" }, // 2 год/класс
            new() { CreatedAt = ClassDateSeedHelper(2020), Symbol = "В" }, // 3 год/класс
            new() { CreatedAt = ClassDateSeedHelper(2019), Symbol = "Г" }, // 4 год/класс
            new() { CreatedAt = ClassDateSeedHelper(2018), Symbol = "Д" }, // 5 год/класс
            new() { CreatedAt = ClassDateSeedHelper(2017), Symbol = "А" }, // 6 год/класс
            new() { CreatedAt = ClassDateSeedHelper(2016), Symbol = "Б" }, // 7 год/класс
            new() { CreatedAt = ClassDateSeedHelper(2015), Symbol = "В" }, // 8 год/класс
            new() { CreatedAt = ClassDateSeedHelper(2014), Symbol = "Г" }, // 9 год/класс
            new() { CreatedAt = ClassDateSeedHelper(2013), Symbol = "Д" }, // 10 год/класс
            new() { CreatedAt = ClassDateSeedHelper(2012), Symbol = "А" }, // 11 год/класс
        };

        await _context.AddRangeAsync(schoolClasses);
        await _context.SaveChangesAsync();
    }

    // ============================================================================================================== //

    private Role RoleSeedHelper(string role)
    {
        return new Role()
        {
            Name = role,
            NormalizedName = role.ToUpper(),
            ConcurrencyStamp = Convert.ToString(DateTimeOffset.Now.Ticks),
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now
        };
    }

    private DateTimeOffset ClassDateSeedHelper(int year)
    {
        return new DateTimeOffset(year, 0, 0, 0, 0, 0, DateTimeOffset.Now.Offset);
    }
}