namespace ElectronicDiary.Dto.Timetable;

public class UpdateTimetableDto
{
    public DateTime? StartedAt { get; set; }
    public int? SchoolClassId { get; set; }
    public int? SubjectId { get; set; }
}