namespace ElectronicDiary.Constants;

public class Role
{
    /// <summary>
    /// Администратор
    /// </summary>
    public const string Admin = "admin";

    /// <summary>
    /// Завуч
    /// </summary>
    public const string HeadTeacher = "head teacher";

    /// <summary>
    /// Учитель
    /// </summary>
    public const string Teacher = "teacher";

    /// <summary>
    /// Ученик
    /// </summary>
    public const string Student = "student";

    /// <summary>
    /// Родитель
    /// </summary>
    public const string Parent = "parent";

    // Костыль для более удобной передачи нескольких ролей для авторизации //
    
    /// <summary>
    /// 
    /// </summary>
    public const string ForAdmins = $"{Admin},{HeadTeacher}";

    /// <summary>
    /// 
    /// </summary>
    public const string ForTeacher = $"{Admin},{HeadTeacher},{Teacher}";

    /// <summary>
    /// Все роли
    /// </summary>
    public const string ForAll = $"{Admin},{HeadTeacher},{Teacher},{Student},{Parent}";
}