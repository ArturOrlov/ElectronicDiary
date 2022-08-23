using Microsoft.AspNetCore.Identity;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Связь Пользователь - Роль
/// </summary>
public class UserRole : IdentityUserRole<int>
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}