using Microsoft.AspNetCore.Identity;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Роль
/// </summary>
public class Role : IdentityRole<int>
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}