using ElectronicDiary.Dto.PerformanceRating;
using ElectronicDiary.Dto.Subject;

namespace ElectronicDiary.Dto.Report;

public class ResponsePerformanceRatingReportDto
{
    public GetSubjectDto Subject { get; set; }
    public GetPerformanceRatingBaseDto Rating { get; set; }
}