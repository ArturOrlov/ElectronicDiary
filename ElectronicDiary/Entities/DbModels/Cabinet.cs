using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Кабинет
/// </summary>
public class Cabinet : BaseModel
{
    public string Number { get; set; }
}