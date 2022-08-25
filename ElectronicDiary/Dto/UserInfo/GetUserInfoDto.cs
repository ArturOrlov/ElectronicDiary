﻿namespace ElectronicDiary.Dto.UserInfo;

public class GetUserInfoDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime? Birthday { get; set; }
    public string PassportNumber { get; set; }
    public string PassportSeries { get; set; }
}