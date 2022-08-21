using ElectronicDiary.Dto.Homework;
using ElectronicDiary.Entities;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controller.Homework;

[ApiController]
[Route("api/homework")]
public class HomeworkController : ControllerBase
{
    private readonly IHomeworkService _homeworkService;

    public HomeworkController(IHomeworkService homeworkService)
    {
        _homeworkService = homeworkService;
    }
    
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

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить домашние задание по филтрам",
        Description = "Получить домашние задание по филтрам",
        OperationId = "Homework.Get",
        Tags = new[] { "Homework" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _homeworkService.GetHomeworkByPaginationAsync(request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

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

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

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

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

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

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}