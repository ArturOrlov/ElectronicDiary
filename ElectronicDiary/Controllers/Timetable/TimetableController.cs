﻿using ElectronicDiary.Dto.Timetable;
using ElectronicDiary.Entities;
using ElectronicDiary.Extension;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controllers.Timetable;

[Authorize]
[ApiController]
[Route("api/timetable")]
public class TimetableController : ControllerBaseExtension
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

        return Response(response);
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить расписание по филтрам",
        Description = "Получить расписание по филтрам",
        OperationId = "Timetable.Get.List",
        Tags = new[] { "Timetable" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _timetableService.GetTimetableByPaginationAsync(request);

        return Response(response);
    }
    
    [HttpGet]
    [Route("date")]
    [SwaggerOperation(
        Summary = "Получить расписание на указанную дату",
        Description = "Получить расписание на указанную дату",
        OperationId = "Timetable.Get.ByDate",
        Tags = new[] { "Timetable" })]
    public async Task<IActionResult> Get([FromQuery] DateTimeOffset date)
    {
        var response = await _timetableService.GetTimetableByTimeAsync(date);

        return Response(response);
    }
    
    [HttpPost]
    [Route("date/excel/download")]
    [SwaggerOperation(
        Summary = "Получить расписание на указанную дату в виде файла Excel",
        Description = "Получить расписание на указанную дату в виде файла Excel",
        OperationId = "Timetable.Get.Excel.ByDate",
        Tags = new[] { "Timetable" })]
    public async Task<IActionResult> GetExcel([FromQuery] DateTimeOffset date)
    {
        var response = await _timetableService.GetTimetableByTimeExcelAsync(date);

        return response.Data;
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

        return Response(response);
    }

    [HttpPut]
    [Route("{timetableId:int}")]
    [SwaggerOperation(
        Summary = "Обновить расписание по его id",
        Description = "Обновить расписание по его id",
        OperationId = "Timetable.Update.ById",
        Tags = new[] { "Timetable" })]
    public async Task<IActionResult> Update([FromRoute] int timetableId, [FromBody] UpDateTimeOffsettableDto request)
    {
        var response = await _timetableService.UpDateTimeOffsettableByIdAsync(timetableId, request);

        return Response(response);
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

        return Response(response);
    }
}