using ElectronicDiary.Dto.UserClass;
using ElectronicDiary.Entities;
using ElectronicDiary.Extension;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controllers.UserClass;

[Authorize]
[ApiController]
[Route("api/user-class")]
public class UserClassController : ControllerBaseExtension
{
    private readonly IUserClassService _userClassService;

    public UserClassController(IUserClassService userClassService)
    {
        _userClassService = userClassService;
    }

    [HttpGet]
    [Route("{userClassId:int}")]
    [SwaggerOperation(
        Summary = "Получить связь пользователь-класс по его id",
        Description = "Получить связь пользователь-класс по его id",
        OperationId = "UserClass.Get.ById",
        Tags = new[] { "UserClass" })]
    public async Task<IActionResult> GetById([FromRoute] int userClassId)
    {
        var response = await _userClassService.GetUserClassByIdAsync(userClassId);

        return Response(response);
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить связь пользователь-класс по филтрам",
        Description = "Получить связь пользователь-класс по филтрам",
        OperationId = "UserClass.Get.List",
        Tags = new[] { "UserClass" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _userClassService.GetUserClassByPaginationAsync(request);

        return Response(response);
    }

    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать связь пользователь-класс",
        Description = "Создать связь пользователь-класс",
        OperationId = "UserClass.Create",
        Tags = new[] { "UserClass" })]
    public async Task<IActionResult> Create([FromBody] CreateUserClassDto request)
    {
        var response = await _userClassService.CreateUserClassAsync(request);

        return Response(response);
    }

    [HttpPut]
    [Route("{userClassId:int}")]
    [SwaggerOperation(
        Summary = "Обновить связь пользователь-класс по его id",
        Description = "Обновить связь пользователь-класс по его id",
        OperationId = "UserClass.Update.ById",
        Tags = new[] { "UserClass" })]
    public async Task<IActionResult> Update([FromRoute] int userClassId, [FromBody] UpdateUserClassDto request)
    {
        var response = await _userClassService.UpdateUserClassByIdAsync(userClassId, request);

        return Response(response);
    }

    [HttpDelete]
    [Route("{userClassId:int}")]
    [SwaggerOperation(
        Summary = "Удалить связь пользователь-класс по его id",
        Description = "Удалить связь пользователь-класс по его id",
        OperationId = "UserClass.Delete.ById",
        Tags = new[] { "UserClass" })]
    public async Task<IActionResult> Delete([FromRoute] int userClassId)
    {
        var response = await _userClassService.DeleteUserClassByIdAsync(userClassId);

        return Response(response);
    }
}