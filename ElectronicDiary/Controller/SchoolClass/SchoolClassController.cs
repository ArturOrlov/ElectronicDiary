using ElectronicDiary.Dto.SchoolClass;
using ElectronicDiary.Entities;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controller.SchoolClass;

[ApiController]
[Route("api/school-class")]
public class SchoolClassController : ControllerBase
{
    private readonly ISchoolClassService _schoolClassService;

    public SchoolClassController(ISchoolClassService schoolClassService)
    {
        _schoolClassService = schoolClassService;
    }

    [HttpGet]
    [Route("{schoolClassId:int}")]
    [SwaggerOperation(
        Summary = "Получить класс по его id",
        Description = "Получить класс по его id",
        OperationId = "SchoolClass.Get.ById",
        Tags = new[] { "SchoolClass" })]
    public async Task<IActionResult> GetById([FromRoute] int schoolClassId)
    {
        var response = await _schoolClassService.GetSchoolClassByIdAsync(schoolClassId);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить классы по филтрам",
        Description = "Получить классы по филтрам",
        OperationId = "SchoolClass.Get",
        Tags = new[] { "SchoolClass" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _schoolClassService.GetSchoolClassByPaginationAsync(request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать класс",
        Description = "Создать класс",
        OperationId = "SchoolClass.Create",
        Tags = new[] { "SchoolClass" })]
    public async Task<IActionResult> Create([FromBody] CreateSchoolClassDto request)
    {
        var response = await _schoolClassService.CreateSchoolClassAsync(request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpPut]
    [Route("{schoolClassId:int}")]
    [SwaggerOperation(
        Summary = "Обновить класс по его id",
        Description = "Обновить класс по его id",
        OperationId = "SchoolClass.Update.ById",
        Tags = new[] { "SchoolClass" })]
    public async Task<IActionResult> Update([FromRoute] int schoolClassId, [FromBody] UpdateSchoolClassDto request)
    {
        var response = await _schoolClassService.UpdateSchoolClassByIdAsync(schoolClassId, request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpDelete]
    [Route("{schoolClassId:int}")]
    [SwaggerOperation(
        Summary = "Удалить класс по его id",
        Description = "Удалить класс по его id",
        OperationId = "SchoolClass.Delete.ById",
        Tags = new[] { "SchoolClass" })]
    public async Task<IActionResult> Delete([FromRoute] int schoolClassId)
    {
        var response = await _schoolClassService.DeleteSchoolClassByIdAsync(schoolClassId);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}