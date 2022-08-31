using ElectronicDiary.Dto.Cabinet;
using ElectronicDiary.Entities;
using ElectronicDiary.Extension;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controllers.Cabinet;

[Authorize]
[ApiController]
[Route("api/cabinet")]
public class CabinetController : ControllerBaseExtension
{
    private readonly ICabinetService _cabinetService;

    public CabinetController(ICabinetService cabinetService)
    {
        _cabinetService = cabinetService;
    }

    [Authorize(Roles = Constants.Role.ForAdmins)]
    [HttpGet]
    [Route("{cabinetId:int}")]
    [SwaggerOperation(
        Summary = "Получить кабинет по его id",
        Description = "Получить кабинет по его id",
        OperationId = "Cabinet.Get.ById",
        Tags = new[] { "Cabinet" })]
    public async Task<IActionResult> GetById([FromRoute] int cabinetId)
    {
        var response = await _cabinetService.GetCabinetByIdAsync(cabinetId);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForAdmins)]
    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить кабинеты по филтрам",
        Description = "Получить кабинеты по филтрам",
        OperationId = "Cabinet.Get.List",
        Tags = new[] { "Cabinet" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _cabinetService.GetCabinetByPaginationAsync(request);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForAdmins)]
    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать кабинет",
        Description = "Создать кабинет",
        OperationId = "Cabinet.Create",
        Tags = new[] { "Cabinet" })]
    public async Task<IActionResult> Create([FromBody] CreateCabinetDto request)
    {
        var response = await _cabinetService.CreateCabinetAsync(request);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForAdmins)]
    [HttpPut]
    [Route("{cabinetId:int}")]
    [SwaggerOperation(
        Summary = "Обновить кабинет по его id",
        Description = "Обновить кабинет по его id",
        OperationId = "Cabinet.Update.ById",
        Tags = new[] { "Cabinet" })]
    public async Task<IActionResult> Update([FromRoute] int cabinetId, [FromBody] UpdateCabinetDto request)
    {
        var response = await _cabinetService.UpdateCabinetByIdAsync(cabinetId, request);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForAdmins)]
    [HttpDelete]
    [Route("{cabinetId:int}")]
    [SwaggerOperation(
        Summary = "Удалить кабинет по его id",
        Description = "Удалить кабинет по его id",
        OperationId = "Cabinet.Delete.ById",
        Tags = new[] { "Cabinet" })]
    public async Task<IActionResult> Delete([FromRoute] int cabinetId)
    {
        var response = await _cabinetService.DeleteCabinetByIdAsync(cabinetId);

        return Response(response);
    }
}