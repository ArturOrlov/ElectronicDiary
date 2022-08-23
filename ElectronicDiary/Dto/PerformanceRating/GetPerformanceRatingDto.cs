namespace ElectronicDiary.Dto.PerformanceRating;

public class GetPerformanceRatingDto
{
    public int Id { get; set; }
    /// <summary>
    /// Оценка. Подумать насчёт реализации + и -
    /// </summary>
    public uint Valuation { get; set; }
    public int TeacherId { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
}