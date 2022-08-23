namespace ElectronicDiary.Dto.Timetable;

public class CreateTimetableDto
{
    public DateTime StartedAt { get; set; }
    public int SchoolClassId { get; set; }
    public int SubjectId { get; set; }
}