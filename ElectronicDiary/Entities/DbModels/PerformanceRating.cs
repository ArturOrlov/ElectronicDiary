using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Оценка по предмету
/// </summary>
public class PerformanceRating : BaseModel
{
    /// <summary>
    /// Оценка
    /// </summary>
    public uint Valuation { get; set; }
    
    public int TeacherId { get; set; }
    public User TeacherUser { get; set; }
    
    public int StudentId { get; set; }
    public User StudentUser { get; set; }
    
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
}