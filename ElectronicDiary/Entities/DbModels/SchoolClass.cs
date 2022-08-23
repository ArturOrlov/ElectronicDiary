using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Школьный класс
/// </summary>
public class SchoolClass : BaseModel
{
    public DateTime ClassCreateTime { get; set; }
    public string Symbol { get; set; }
}