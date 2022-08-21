namespace ElectronicDiary.Entities.Base;

public class BaseResponse<T> where T : class
{
    public bool IsError { get; set; }
    public string Description { get; set; }
    public T Data { get; set; }
}