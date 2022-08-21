using ElectronicDiary.Dto.Timetable;
using ElectronicDiary.Entities;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controller.Timetable;

[ApiController]
[Route("api/timetable")]
public class TimetableController : ControllerBase
{
    private readonly ITimetableService _timetableService;

    public TimetableController(ITimetableService timetableService)
    {
        _timetableService = timetableService;
    }

    [HttpGet]
    [Route("{timetableId:int}")]
    [SwaggerOperation(
        Summary = "Получить расписание по его id",
        Description = "Получить расписание по его id",
        OperationId = "Timetable.Get.ById",
        Tags = new[] { "Timetable" })]
    public async Task<IActionResult> GetById([FromRoute] int timetableId)
    {
        var response = await _timetableService.GetTimetableByIdAsync(timetableId);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить расписаниеы по филтрам",
        Description = "Получить расписаниеы по филтрам",
        OperationId = "Timetable.Get",
        Tags = new[] { "Timetable" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _timetableService.GetTimetableByPaginationAsync(request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать расписание",
        Description = "Создать расписание",
        OperationId = "Timetable.Create",
        Tags = new[] { "Timetable" })]
    public async Task<IActionResult> Create([FromBody] CreateTimetableDto request)
    {
        var response = await _timetableService.CreateTimetableAsync(request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpPut]
    [Route("{timetableId:int}")]
    [SwaggerOperation(
        Summary = "Обновить расписание по его id",
        Description = "Обновить расписание по его id",
        OperationId = "Timetable.Update.ById",
        Tags = new[] { "Timetable" })]
    public async Task<IActionResult> Update([FromRoute] int timetableId, [FromBody] UpdateTimetableDto request)
    {
        var response = await _timetableService.UpdateTimetableByIdAsync(timetableId, request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpDelete]
    [Route("{timetableId:int}")]
    [SwaggerOperation(
        Summary = "Удалить расписание по его id",
        Description = "Удалить расписание по его id",
        OperationId = "Timetable.Delete.ById",
        Tags = new[] { "Timetable" })]
    public async Task<IActionResult> Delete([FromRoute] int timetableId)
    {
        var response = await _timetableService.DeleteTimetableByIdAsync(timetableId);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}