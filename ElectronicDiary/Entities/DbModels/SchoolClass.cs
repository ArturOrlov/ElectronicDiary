using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Школьный класс
/// </summary>
public class SchoolClass : BaseModel
{
    /// <summary>
    /// Дата создания класса. Для обхода функционала "автоматическое увеличение номера класса"
    /// </summary>
    public DateTimeOffset ClassCreateTime { get; set; }
    
    /// <summary>
    /// Не знал как назвать. Это буква класса, прим "11А" - А
    /// </summary>
    public string Symbol { get; set; }
}