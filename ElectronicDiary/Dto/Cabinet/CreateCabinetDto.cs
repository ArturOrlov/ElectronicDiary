namespace ElectronicDiary.Dto.Cabinet;

public class CreateCabinetDto
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