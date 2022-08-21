using ElectronicDiary.Dto.Role;
using ElectronicDiary.Entities;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controller.Role;

[ApiController]
[Route("api/role")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

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

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить роли по филтрам",
        Description = "Получить роли по филтрам",
        OperationId = "Role.Get",
        Tags = new[] { "Role" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _roleService.GetRoleByPaginationAsync(request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

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

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

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

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

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

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}