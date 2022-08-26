using Microsoft.AspNetCore.Identity;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Связь пользователь-роль
/// </summary>
public class UserRole : IdentityUserRole<int>
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}