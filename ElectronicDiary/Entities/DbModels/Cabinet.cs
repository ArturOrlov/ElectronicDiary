using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Entities.DbModels;

/// <summary>
/// Кабинет
/// </summary>
public class Cabinet : BaseModel
{
    /// <summary>
    /// Номер кабинета
    /// </summary>
    public string Number { get; set; }
}