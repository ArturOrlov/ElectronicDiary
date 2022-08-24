using ElectronicDiary.Dto.SchoolClass;
using ElectronicDiary.Entities;
using ElectronicDiary.Extension;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controllers.SchoolClass;

[Authorize]
[ApiController]
[Route("api/school-class")]
public class SchoolClassController : ControllerBaseExtension
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

        return Response(response);
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить классы по филтрам",
        Description = "Получить классы по филтрам",
        OperationId = "SchoolClass.Get.List",
        Tags = new[] { "SchoolClass" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _schoolClassService.GetSchoolClassByPaginationAsync(request);

        return Response(response);
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

        return Response(response);
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

        return Response(response);
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

        return Response(response);
    }
}