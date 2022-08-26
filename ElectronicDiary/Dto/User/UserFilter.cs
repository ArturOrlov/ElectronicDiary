using ElectronicDiary.Entities;

namespace ElectronicDiary.Dto.User;

public class UserFilter : BasePagination
{
    public int RoleId { get; set; }
}