namespace ElectronicDiary.Dto.Timetable;

public class UpDateTimeOffsettableDto
{
    public DateTimeOffset? StartedAt { get; set; }
    public int? SchoolClassId { get; set; }
    public int? SubjectId { get; set; }
}