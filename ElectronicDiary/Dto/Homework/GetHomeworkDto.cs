﻿namespace ElectronicDiary.Dto.Homework;

public class GetHomeworkDto
{
    public int Id { get; set; }
    public string HomeworkDescription { get; set; }
    public int SubjectId { get; set; }
    public int ClassId { get; set; }
}