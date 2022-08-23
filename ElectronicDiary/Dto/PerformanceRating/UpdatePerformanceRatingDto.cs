namespace ElectronicDiary.Dto.PerformanceRating;

public class UpdatePerformanceRatingDto
{
    /// <summary>
    /// Оценка
    /// </summary>
    public uint? Valuation { get; set; }
    public int? TeacherId { get; set; }
    public int? StudentId { get; set; }
    public int? SubjectId { get; set; }
}