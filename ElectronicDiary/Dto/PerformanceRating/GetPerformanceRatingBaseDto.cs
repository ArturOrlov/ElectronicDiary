namespace ElectronicDiary.Dto.PerformanceRating;

public class GetPerformanceRatingBaseDto
{
    public int Id { get; set; }
    /// <summary>
    /// Оценка. Подумать насчёт реализации + и -
    /// </summary>
    public uint Valuation { get; set; }
}