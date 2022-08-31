using ElectronicDiary.Dto.PerformanceRating;
using ElectronicDiary.Entities;
using ElectronicDiary.Extension;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controllers.PerformanceRating;

[Authorize]
[ApiController]
[Route("api/performance-rating")]
public class PerformanceRatingController : ControllerBaseExtension
{
    private readonly IPerformanceRatingService _performanceRatingControllerService;

    public PerformanceRatingController(IPerformanceRatingService performanceRatingControllerService)
    {
        _performanceRatingControllerService = performanceRatingControllerService;
    }
    
    [Authorize(Roles = Constants.Role.ForTeacher)]
    [HttpGet]
    [Route("{performanceRatingId:int}")]
    [SwaggerOperation(
        Summary = "Получить оценку по его id",
        Description = "Получить оценку по его id",
        OperationId = "PerformanceRating.Get.ById",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> GetById([FromRoute] int performanceRatingId)
    {
        var response = await _performanceRatingControllerService.GetPerformanceRatingByIdAsync(performanceRatingId);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForTeacher)]
    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить оценки по филтрам",
        Description = "Получить оценки по филтрам",
        OperationId = "PerformanceRating.Get.List",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _performanceRatingControllerService.GetPerformanceRatingByPaginationAsync(request);

        return Response(response);
    }
    
    [Authorize(Roles = Constants.Role.ForAll)]
    [HttpGet]
    [Route("self")]
    [SwaggerOperation(
        Summary = "Получить свои оценки",
        Description = "Получить свои оценки",
        OperationId = "PerformanceRating.Get.List.BySelf",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> GetBySelf()
    {
        var response = await _performanceRatingControllerService.GetPerformanceRatingBySelfAsync(HttpContext.GetUserData());

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForTeacher)]
    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать оценку",
        Description = "Создать оценку",
        OperationId = "PerformanceRating.Create",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> Create([FromBody] CreatePerformanceRatingDto request)
    {
        var response = await _performanceRatingControllerService.CreatePerformanceRatingAsync(request);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForTeacher)]
    [HttpPut]
    [Route("{performanceRatingId:int}")]
    [SwaggerOperation(
        Summary = "Обновить оценку по его id",
        Description = "Обновить оценку по его id",
        OperationId = "PerformanceRating.Update.ById",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> Update([FromRoute] int performanceRatingId, [FromBody] UpdatePerformanceRatingDto request)
    {
        var response = await _performanceRatingControllerService.UpdatePerformanceRatingByIdAsync(performanceRatingId, request);

        return Response(response);
    }

    [Authorize(Roles = Constants.Role.ForTeacher)]
    [HttpDelete]
    [Route("{performanceRatingId:int}")]
    [SwaggerOperation(
        Summary = "Удалить оценку по его id",
        Description = "Удалить оценку по его id",
        OperationId = "PerformanceRating.Delete.ById",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> Delete([FromRoute] int performanceRatingId)
    {
        var response = await _performanceRatingControllerService.DeletePerformanceRatingByIdAsync(performanceRatingId);

        return Response(response);
    }
}