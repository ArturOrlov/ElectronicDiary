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
    
    /// <summary>
    /// Корпус
    /// </summary>
    public string Campus { get; set; }
    
    /// <summary>
    /// Этаж
    /// </summary>
    public string Floor { get; set; }
}