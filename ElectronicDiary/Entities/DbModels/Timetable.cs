using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Расписание
/// </summary>
public class Timetable : BaseModel
{
    public DateTimeOffset StartedAt { get; set; }
    
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    public int ClassId { get; set; }
    public SchoolClass SchoolClass { get; set; }
}