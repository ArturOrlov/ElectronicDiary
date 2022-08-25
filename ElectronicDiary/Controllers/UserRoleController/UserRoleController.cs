using ElectronicDiary.Dto.UserRole;
using ElectronicDiary.Entities;
using ElectronicDiary.Extension;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controllers.UserRoleController;

[Authorize]
[ApiController]
[Route("api/user-role")]
public class UserRoleController : ControllerBaseExtension
{
    private readonly IUserRoleService _userRoleService;

    public UserRoleController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    [HttpGet]
    [Route("{userRoleId:int}")]
    [SwaggerOperation(
        Summary = "Получить связь пользователь-роль по его id",
        Description = "Получить связь пользователь-роль по его id",
        OperationId = "UserRole.Get.ById",
        Tags = new[] { "UserRole" })]
    public async Task<IActionResult> GetById([FromRoute] int userRoleId)
    {
        var response = await _userRoleService.GetUserRoleByIdAsync(userRoleId);

        return Response(response);
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить связь пользователь-роль по филтрам",
        Description = "Получить связь пользователь-роль по филтрам",
        OperationId = "UserRole.Get.List",
        Tags = new[] { "UserRole" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _userRoleService.GetUserRoleByPaginationAsync(request);

        return Response(response);
    }

    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать связь пользователь-роль",
        Description = "Создать связь пользователь-роль",
        OperationId = "UserRole.Create",
        Tags = new[] { "UserRole" })]
    public async Task<IActionResult> Create([FromBody] CreateUserRoleDto request)
    {
        var response = await _userRoleService.CreateUserRoleAsync(request);

        return Response(response);
    }

    [HttpPut]
    [Route("{userRoleId:int}")]
    [SwaggerOperation(
        Summary = "Обновить связь пользователь-роль по его id",
        Description = "Обновить связь пользователь-роль по его id",
        OperationId = "UserRole.Update.ById",
        Tags = new[] { "UserRole" })]
    public async Task<IActionResult> Update([FromRoute] int userRoleId, [FromBody] UpdateUserRoleDto request)
    {
        var response = await _userRoleService.UpdateUserRoleByIdAsync(userRoleId, request);

        return Response(response);
    }

    [HttpDelete]
    [Route("{userRoleId:int}")]
    [SwaggerOperation(
        Summary = "Удалить связь пользователь-роль по его id",
        Description = "Удалить связь пользователь-роль по его id",
        OperationId = "UserRole.Delete.ById",
        Tags = new[] { "UserRole" })]
    public async Task<IActionResult> Delete([FromRoute] int userRoleId)
    {
        var response = await _userRoleService.DeleteUserRoleByIdAsync(userRoleId);

        return Response(response);
    }
}