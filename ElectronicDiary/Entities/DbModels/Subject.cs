using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Школьный предмет
/// </summary>
public class Subject : BaseModel
{
    public string Name { get; set; }
}