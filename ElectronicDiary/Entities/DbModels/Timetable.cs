using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Расписание
/// </summary>
public class Timetable : BaseModel
{
    public DateTime StartedAt { get; set; }
    
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    public int SchoolClassId { get; set; }
    public SchoolClass SchoolClass { get; set; }
}