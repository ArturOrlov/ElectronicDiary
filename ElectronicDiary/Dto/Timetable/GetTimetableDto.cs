namespace ElectronicDiary.Dto.Timetable;

public class GetTimetableDto
{
    public DateTimeOffset StartedAt { get; set; }
    public int SubjectId { get; set; }
    public int ClassId { get; set; }
}