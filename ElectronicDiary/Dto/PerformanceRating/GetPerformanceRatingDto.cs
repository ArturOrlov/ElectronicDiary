namespace ElectronicDiary.Dto.PerformanceRating;

public class GetPerformanceRatingDto : GetPerformanceRatingBaseDto
{
    public int TeacherId { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
}