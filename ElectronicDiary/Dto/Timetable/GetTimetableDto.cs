namespace ElectronicDiary.Dto.Timetable;

public class GetTimetableDto
{
    public int Id { get; set; }
    public DateTime StartedAt { get; set; }
    public int SubjectId { get; set; }
    public int ClassId { get; set; }
}