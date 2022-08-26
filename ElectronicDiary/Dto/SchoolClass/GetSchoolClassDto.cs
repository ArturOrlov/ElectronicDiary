namespace ElectronicDiary.Dto.SchoolClass;

public class GetSchoolClassDto
{
    public int Id { get; set; }
    public DateTimeOffset ClassCreateTime { get; set; }
    public string Symbol { get; set; }
}