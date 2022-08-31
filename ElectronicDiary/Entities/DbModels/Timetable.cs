using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Расписание
/// </summary>
public class Timetable : BaseModel
{
    /// <summary>
    /// Начало урока
    /// </summary>
    public DateTimeOffset StartedAt { get; set; }
    
    /// <summary>
    /// Длительность урока
    /// </summary>
    public DateTimeOffset LessonDuration { get; set; }
    
    /// <summary>
    /// Длительность перемены
    /// </summary>
    public DateTimeOffset BreakDuration { get; set; }
    
    /// <summary>
    /// Id предмета
    /// </summary>
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    /// <summary>
    /// Id класса
    /// </summary>
    public int SchoolClassId { get; set; }
    public SchoolClass SchoolClass { get; set; }
}