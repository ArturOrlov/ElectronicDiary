using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

public class UserClass : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int SchoolClassId { get; set; }
    public SchoolClass SchoolClass { get; set; }
    
    /// <summary>
    /// Флаг указывающий что данный пользователь является классным руководителем этого класса
    /// </summary>
    public bool IsClassroomTeacher { get; set; }
}