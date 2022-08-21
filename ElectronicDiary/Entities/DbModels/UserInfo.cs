using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

public class UserInfo : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    
    public DateTime? Birthday { get; set; }
    
    public string PassportNumber { get; set; }
    public string PassportSeries { get; set; }
}