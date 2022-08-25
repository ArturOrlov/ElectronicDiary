namespace ElectronicDiary.Dto.Report;

public class ResponseSubjectReportDto
{
    public int ClassId { get; set; }
    public int SubjectId { get; set; }
    
    /// <summary>
    /// Средний балл
    /// </summary>
    public float Gpa { get; set; }
}