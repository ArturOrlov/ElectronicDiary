using ElectronicDiary.Dto.Report;
using ElectronicDiary.Extension;
using ElectronicDiary.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controllers._Dev;

[Authorize]
[ApiController]
[Route("api/report")]
public class ReportController : ControllerBaseExtension
{
    private readonly IPerformanceRatingService _performanceRatingService;
    private readonly ISchoolClassService _schoolClassService;
    private readonly ISubjectService _subjectService;

    public ReportController(IPerformanceRatingService performanceRatingService, 
        ISchoolClassService schoolClassService,
        ISubjectService subjectService)
    {
        _performanceRatingService = performanceRatingService;
        _schoolClassService = schoolClassService;
        _subjectService = subjectService;
    }

    [HttpGet]
    [Route("student")]
    [SwaggerOperation(
        Summary = "Отчёт по ученику. Все предметы с оценками за выбранный год",
        Description = "Отчёт по ученику. Все предметы с оценками за выбранный год",
        OperationId = "Report.Get.Student",
        Tags = new[] { "Report" })]
    public async Task<IActionResult> GetReportByStudentId([FromQuery] GetPerformanceRatingReportDto request)
    {
        var response = await _performanceRatingService.GetPerformanceRatingReportAsync(request);

        return Response(response);
    }

    [HttpGet]
    [Route("school-class")]
    [SwaggerOperation(
        Summary = "Отчёт по классам и их успевавемости по выбраным предметам",
        Description = "Отчёт по классам и их успевавемости по выбраным предметам",
        OperationId = "Report.Get.SchoolClass",
        Tags = new[] { "Report" })]
    public async Task<IActionResult> GetReportBySchoolClassId([FromQuery] GetSchoolClassReportDto request)
    {
        var response = await _schoolClassService.GetSchoolClassReportAsync(request);

        return Response(response);
    }

    [HttpGet]
    [Route("subject")]
    [SwaggerOperation(
        Summary = "Отчёт по предмету. Все классы в виде топа среднего балла",
        Description = "Отчёт по предмету. Все классы в виде топа среднего балла",
        OperationId = "Report.Get.Subject",
        Tags = new[] { "Report" })]
    public async Task<IActionResult> GetReportBySubjectId([FromQuery] GetSubjectReportDto request)
    {
        var response = await _subjectService.GetSubjectReportAsync(request);

        return Response(response);
    }
}