namespace ElectronicDiary.Dto.Timetable;

public class GetTimetableExcelDto
{
    public int Id { get; set; }
    public DateTimeOffset StartedAt { get; set; }
    public DateTimeOffset LessonDuration { get; set; }
    public DateTimeOffset BreakDuration { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public int SchoolClassId { get; set; }
    public string SchoolClassName { get; set; }
    public DateTimeOffset ClassCreateTime { get; set; }
}