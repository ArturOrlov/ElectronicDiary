using ElectronicDiary.Constants;
using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

    public void SeedDb()
    {
        SeedRoles();
        SeedAdmin();
    }

    private void SeedRoles()
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
        
        _context.AddRange(roles);
        _context.SaveChanges();
    }

    private async Task SeedAdmin()
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
}