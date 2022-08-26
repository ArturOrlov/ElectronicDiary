using Microsoft.AspNetCore.Identity;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Пользователь
/// </summary>
public class User : IdentityUser<int>
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset RefreshTokenExpiryTime { get; set; }
}