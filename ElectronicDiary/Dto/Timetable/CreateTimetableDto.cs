namespace ElectronicDiary.Dto.Timetable;

public class CreateTimetableDto
{
    public DateTimeOffset StartedAt { get; set; }
    public int SchoolClassId { get; set; }
    public int ClassId { get; set; }
}