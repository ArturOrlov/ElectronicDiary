using ElectronicDiary.Dto.Homework;
using ElectronicDiary.Entities;
using ElectronicDiary.Extension;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controllers.Homework;

[Authorize]
[ApiController]
[Route("api/homework")]
public class HomeworkController : ControllerBaseExtension
{
    private readonly IHomeworkService _homeworkService;

    public HomeworkController(IHomeworkService homeworkService)
    {
        _homeworkService = homeworkService;
    }
    
    [Authorize(Roles = Constants.Role.ForTeacher)]
    [HttpGet]
    [Route("{homeworkId:int}")]
    [SwaggerOperation(
        Summary = "Получить домашние задание по его id",
        Description = "Получить домашние задание по его id",
        OperationId = "Homework.Get.ById",
        Tags = new[] { "Homework" })]
    public async Task<IActionResult> GetById([FromRoute] int homeworkId)
    {
        var response = await _homeworkService.GetHomeworkByIdAsync(homeworkId);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForTeacher)]
    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить домашние задания по филтрам",
        Description = "Получить домашние задания по филтрам",
        OperationId = "Homework.Get.List",
        Tags = new[] { "Homework" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _homeworkService.GetHomeworkByPaginationAsync(request);

        return Response(response);
    }
    
    [Authorize(Roles = Constants.Role.ForAll)]
    [HttpGet]
    [Route("self")]
    [SwaggerOperation(
        Summary = "Получить свои оценки",
        Description = "Получить свои оценки",
        OperationId = "PerformanceRating.Get.List.BySelf",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> GetBySelf()
    {
        var response = await _homeworkService.GetHomeworkBySelfAsync(HttpContext.GetUserData());

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForTeacher)]
    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать домашние задание",
        Description = "Создать домашние задание",
        OperationId = "Homework.Create",
        Tags = new[] { "Homework" })]
    public async Task<IActionResult> Create([FromBody] CreateHomeworkDto request)
    {
        var response = await _homeworkService.CreateHomeworkAsync(request);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForTeacher)]
    [HttpPut]
    [Route("{homeworkId:int}")]
    [SwaggerOperation(
        Summary = "Обновить домашние задание по его id",
        Description = "Обновить домашние задание по его id",
        OperationId = "Homework.Update.ById",
        Tags = new[] { "Homework" })]
    public async Task<IActionResult> Update([FromRoute] int homeworkId, [FromBody] UpdateHomeworkDto request)
    {
        var response = await _homeworkService.UpdateHomeworkByIdAsync(homeworkId, request);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForTeacher)]
    [HttpDelete]
    [Route("{homeworkId:int}")]
    [SwaggerOperation(
        Summary = "Удалить домашние задание по его id",
        Description = "Удалить домашние задание по его id",
        OperationId = "Homework.Delete.ById",
        Tags = new[] { "Homework" })]
    public async Task<IActionResult> Delete([FromRoute] int homeworkId)
    {
        var response = await _homeworkService.DeleteHomeworkByIdAsync(homeworkId);

        return Response(response);
    }
}