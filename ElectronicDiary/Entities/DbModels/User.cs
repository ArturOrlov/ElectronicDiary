﻿using Microsoft.AspNetCore.Identity;

namespace ElectronicDiary.Entities.DbModels;

public class User : IdentityUser<int>
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}