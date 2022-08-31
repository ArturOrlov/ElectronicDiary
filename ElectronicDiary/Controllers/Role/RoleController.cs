using ElectronicDiary.Dto.Role;
using ElectronicDiary.Entities;
using ElectronicDiary.Extension;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controllers.Role;

[Authorize]
[ApiController]
[Route("api/role")]
public class RoleController : ControllerBaseExtension
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [Authorize(Roles = Constants.Role.Admin)]
    [HttpGet]
    [Route("{roleId:int}")]
    [SwaggerOperation(
        Summary = "Получить роль по его id",
        Description = "Получить роль по его id",
        OperationId = "Role.Get.ById",
        Tags = new[] { "Role" })]
    public async Task<IActionResult> GetById([FromRoute] int roleId)
    {
        var response = await _roleService.GetRoleByIdAsync(roleId);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.Admin)]
    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить роли по филтрам",
        Description = "Получить роли по филтрам",
        OperationId = "Role.Get.List",
        Tags = new[] { "Role" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _roleService.GetRoleByPaginationAsync(request);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.Admin)]
    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать роль",
        Description = "Создать роль",
        OperationId = "Role.Create",
        Tags = new[] { "Role" })]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto request)
    {
        var response = await _roleService.CreateRoleAsync(request);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.Admin)]
    [HttpPut]
    [Route("{roleId:int}")]
    [SwaggerOperation(
        Summary = "Обновить роль по его id",
        Description = "Обновить роль по его id",
        OperationId = "Role.Update.ById",
        Tags = new[] { "Role" })]
    public async Task<IActionResult> Update([FromRoute] int roleId, [FromBody] UpdateRoleDto request)
    {
        var response = await _roleService.UpdateRoleByIdAsync(roleId, request);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.Admin)]
    [HttpDelete]
    [Route("{roleId:int}")]
    [SwaggerOperation(
        Summary = "Удалить роль по его id",
        Description = "Удалить роль по его id",
        OperationId = "Role.Delete.ById",
        Tags = new[] { "Role" })]
    public async Task<IActionResult> Delete([FromRoute] int roleId)
    {
        var response = await _roleService.DeleteRoleByIdAsync(roleId);

        return Response(response);
    }
}