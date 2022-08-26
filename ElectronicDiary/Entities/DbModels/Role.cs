using Microsoft.AspNetCore.Identity;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Роль
/// </summary>
public class Role : IdentityRole<int>
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}