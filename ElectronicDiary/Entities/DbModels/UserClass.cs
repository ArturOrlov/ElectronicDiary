using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

public class UserClass : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int SchoolClassId { get; set; }
    public SchoolClass SchoolClass { get; set; }
    
    public bool IsClassroomTeacher { get; set; }
}