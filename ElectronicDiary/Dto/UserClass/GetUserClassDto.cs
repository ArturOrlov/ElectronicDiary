namespace ElectronicDiary.Dto.UserClass;

public class GetUserClassDto
{
    public int UserId { get; set; }
    public int SchoolClassId { get; set; }
    public bool IsClassroomTeacher { get; set; }
}