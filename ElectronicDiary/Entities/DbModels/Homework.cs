using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Домашнее задание
/// </summary>
public class Homework : BaseModel
{
    /// <summary>
    /// В задаче написанно "Кэшировать домашние задания на следующий день" и "следующий день" меня смещает,
    /// из этого можно решить что дз всегда должно быть на следующий день
    /// </summary>
    public DateTime ForDateAt { get; set; }
    
    public string HomeworkDescription { get; set; }
    
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    public int SchoolClassId { get; set; }
    public SchoolClass SchoolClass { get; set; }
}