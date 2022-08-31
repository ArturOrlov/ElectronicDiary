namespace ElectronicDiary.Entities;

public class BasePagination
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 15;
}