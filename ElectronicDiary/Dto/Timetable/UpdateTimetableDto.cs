namespace ElectronicDiary.Dto.Timetable;

public class UpdateTimetableDto
{
    public DateTimeOffset StartedAt { get; set; }
    public int? SubjectId { get; set; }
    public int? ClassId { get; set; }
}