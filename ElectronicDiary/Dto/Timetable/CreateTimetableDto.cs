namespace ElectronicDiary.Dto.Timetable;

public class CreateTimetableDto
{
    public DateTimeOffset StartedAt { get; set; }
    public DateTimeOffset LessonDuration { get; set; }
    public DateTimeOffset BreakDuration { get; set; }

    public int SchoolClassId { get; set; }
    public int SubjectId { get; set; }
}