using ElectronicDiary.Context;
using ElectronicDiary.Extension;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controller._Dev;

[ApiController]
[Route("api/dev")]
public class DevController : ControllerBaseExtension
{
    private readonly ElectronicDiaryDbContext _context;
    private readonly UserManager<Entities.DbModels.User> _userManager;

    public DevController(ElectronicDiaryDbContext context,
        UserManager<Entities.DbModels.User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("seed-db")]
    [SwaggerOperation(
        Summary = "Заполнить дб данными",
        Description = "Заполнить дб данными",
        OperationId = "Dev.Post.Db",
        Tags = new[] { "Dev" })]
    public async Task<IActionResult> PostSeedDb()
    {
        var bootstrap = new Bootstrap.Bootstrap(_context, _userManager);

        await bootstrap.SeedDb();

        return Ok();
    }
}