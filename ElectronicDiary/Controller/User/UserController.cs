using ElectronicDiary.Dto.User;
using ElectronicDiary.Entities;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controller.User;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("{userId:int}")]
    [SwaggerOperation(
        Summary = "Получить пользователя по его id",
        Description = "Получить пользователя по его id",
        OperationId = "User.Get.ById",
        Tags = new[] { "User" })]
    public async Task<IActionResult> GetById([FromRoute] int userId)
    {
        var response = await _userService.GetUserByIdAsync(userId);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить пользователяы по филтрам",
        Description = "Получить пользователяы по филтрам",
        OperationId = "User.Get",
        Tags = new[] { "User" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _userService.GetUserByPaginationAsync(request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать пользователя",
        Description = "Создать пользователя",
        OperationId = "User.Create",
        Tags = new[] { "User" })]
    public async Task<IActionResult> Create([FromBody] CreateUserDto request)
    {
        var response = await _userService.CreateUserAsync(request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpPut]
    [Route("{userId:int}")]
    [SwaggerOperation(
        Summary = "Обновить пользователя по его id",
        Description = "Обновить пользователя по его id",
        OperationId = "User.Update.ById",
        Tags = new[] { "User" })]
    public async Task<IActionResult> Update([FromRoute] int userId, [FromBody] UpdateUserDto request)
    {
        var response = await _userService.UpdateUserByIdAsync(userId, request);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpDelete]
    [Route("{userId:int}")]
    [SwaggerOperation(
        Summary = "Удалить пользователя по его id",
        Description = "Удалить пользователя по его id",
        OperationId = "User.Delete.ById",
        Tags = new[] { "User" })]
    public async Task<IActionResult> Delete([FromRoute] int userId)
    {
        var response = await _userService.DeleteUserByIdAsync(userId);

        if (response.IsError)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}