namespace ElectronicDiary.Entities.Base;

public class BaseModel
{
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}