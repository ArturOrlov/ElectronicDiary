﻿using ElectronicDiary.Dto.Subject;
using ElectronicDiary.Entities;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controller.Subject;


[ApiController]
[Route("api/subject")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    [Route("{subjectId:int}")]
    [SwaggerOperation(
        Summary = "Получить школьный предмет по его id",
        Description = "Получить школьный предмет по его id",
        OperationId = "Subject.Get.ById",
        Tags = new[] { "Subject" })]
    public async Task<IActionResult> GetById([FromRoute] int subjectId)
    {
        var response = await _subjectService.GetSubjectByIdAsync(subjectId);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить школьный предметы по филтрам",
        Description = "Получить школьный предметы по филтрам",
        OperationId = "Subject.Get",
        Tags = new[] { "Subject" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _subjectService.GetSubjectByPaginationAsync(request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать школьный предмет",
        Description = "Создать школьный предмет",
        OperationId = "Subject.Create",
        Tags = new[] { "Subject" })]
    public async Task<IActionResult> Create([FromBody] CreateSubjectDto request)
    {
        var response = await _subjectService.CreateSubjectAsync(request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpPut]
    [Route("{subjectId:int}")]
    [SwaggerOperation(
        Summary = "Обновить школьный предмет по его id",
        Description = "Обновить школьный предмет по его id",
        OperationId = "Subject.Update.ById",
        Tags = new[] { "Subject" })]
    public async Task<IActionResult> Update([FromRoute] int subjectId, [FromBody] UpdateSubjectDto request)
    {
        var response = await _subjectService.UpdateSubjectByIdAsync(subjectId, request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpDelete]
    [Route("{subjectId:int}")]
    [SwaggerOperation(
        Summary = "Удалить школьный предмет по его id",
        Description = "Удалить школьный предмет по его id",
        OperationId = "Subject.Delete.ById",
        Tags = new[] { "Subject" })]
    public async Task<IActionResult> Delete([FromRoute] int subjectId)
    {
        var response = await _subjectService.DeleteSubjectByIdAsync(subjectId);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}