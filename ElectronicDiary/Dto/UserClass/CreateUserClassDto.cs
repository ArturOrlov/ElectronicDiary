namespace ElectronicDiary.Dto.UserClass;

public class CreateUserClassDto
{
    public int UserId { get; set; }
    public int SchoolClassId { get; set; }
    public bool IsClassroomTeacher { get; set; }
}