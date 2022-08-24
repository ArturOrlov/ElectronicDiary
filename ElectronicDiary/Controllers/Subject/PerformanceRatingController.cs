using ElectronicDiary.Dto.PerformanceRating;
using ElectronicDiary.Entities;
using ElectronicDiary.Extension;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controllers.Subject;

[Authorize]
[ApiController]
[Route("api/performance-rating")]
public class PerformanceRatingController : ControllerBaseExtension
{
    private readonly IPerformanceRatingService _performanceRatingService;

    public PerformanceRatingController(IPerformanceRatingService performanceRatingService)
    {
        _performanceRatingService = performanceRatingService;
    }
    
    [HttpGet]
    [Route("{performanceRatingId:int}")]
    [SwaggerOperation(
        Summary = "Получить оценку по предмету по его id",
        Description = "Получить оценку по предмету по его id",
        OperationId = "PerformanceRating.Get.ById",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> GetById([FromRoute] int performanceRatingId)
    {
        var response = await _performanceRatingService.GetPerformanceRatingByIdAsync(performanceRatingId);

        return Response(response);
    }

    [HttpGet]
    [Route("")]
    [SwaggerOperation(
        Summary = "Получить оценки по предмету по филтрам",
        Description = "Получить оценки по предмету по филтрам",
        OperationId = "PerformanceRating.Get.List",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> GetAll([FromQuery] BasePagination request)
    {
        var response = await _performanceRatingService.GetPerformanceRatingByPaginationAsync(request);

        return Response(response);
    }

    [HttpPost]
    [Route("")]
    [SwaggerOperation(
        Summary = "Создать оценку по предмету",
        Description = "Создать оценку по предмету",
        OperationId = "PerformanceRating.Create",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> Create([FromBody] CreatePerformanceRatingDto request)
    {
        var response = await _performanceRatingService.CreatePerformanceRatingAsync(request);

        return Response(response);
    }

    [HttpPut]
    [Route("{performanceRatingId:int}")]
    [SwaggerOperation(
        Summary = "Обновить оценку по предмету по его id",
        Description = "Обновить оценку по предмету по его id",
        OperationId = "PerformanceRating.Update.ById",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> Update([FromRoute] int performanceRatingId, [FromBody] UpdatePerformanceRatingDto request)
    {
        var response = await _performanceRatingService.UpdatePerformanceRatingByIdAsync(performanceRatingId, request);

        return Response(response);
    }

    [HttpDelete]
    [Route("{performanceRatingId:int}")]
    [SwaggerOperation(
        Summary = "Удалить оценку по предмету по его id",
        Description = "Удалить оценку по предмету по его id",
        OperationId = "PerformanceRating.Delete.ById",
        Tags = new[] { "PerformanceRating" })]
    public async Task<IActionResult> Delete([FromRoute] int performanceRatingId)
    {
        var response = await _performanceRatingService.DeletePerformanceRatingByIdAsync(performanceRatingId);

        return Response(response);
    }
}