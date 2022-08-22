namespace ElectronicDiary.Dto.Homework;

public class CreateHomeworkDto
{
    public DateTimeOffset ForDateAt { get; set; }
    public string HomeworkDescription { get; set; }
    public int SubjectId { get; set; }
    public int SchoolClassId { get; set; }
}