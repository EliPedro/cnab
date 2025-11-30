using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSite.Services;

namespace WebSite.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImportController : ControllerBase
{
    private readonly ICnabImportService _importService;

    public ImportController(ICnabImportService importService)
    {
        _importService = importService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload()
    {
        if (!Request.HasFormContentType) return BadRequest(new { error = "No form content" });

        var form = await Request.ReadFormAsync();
        var file = form.Files.GetFile("file");
        if (file == null) return BadRequest(new { error = "file is required" });

        using var stream = file.OpenReadStream();
        var result = await _importService.ImportFromStreamAsync(stream);
        return Ok(result);
    }
}
