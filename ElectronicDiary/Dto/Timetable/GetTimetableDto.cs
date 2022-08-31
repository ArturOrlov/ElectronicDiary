namespace ElectronicDiary.Dto.Timetable;

public class GetTimetableDto
{
    public int Id { get; set; }
    public DateTimeOffset StartedAt { get; set; }
    public DateTimeOffset LessonDuration { get; set; }
    public DateTimeOffset BreakDuration { get; set; }
    public int SubjectId { get; set; }
    public int SchoolClassId { get; set; }
}