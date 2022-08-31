namespace ElectronicDiary.Dto.UserInfo;

public class UpdateUserInfoDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTimeOffset? Birthday { get; set; }
    public string PassportNumber { get; set; }
    public string PassportSeries { get; set; }
    public bool IsParent { get; set; }
    public List<int>? ChildrenUserId { get; set; }
}