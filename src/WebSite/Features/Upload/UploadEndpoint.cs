using Carter;
using WebSite.Entities;
using WebSite.Shared;

namespace WebSite.Features.Upload;

sealed class ImportSummary
{
    public int TotalLines { get; set; }
    public int ImportedTransactions { get; set; }
    public int ParseErrors { get; set; }
}

public class UploadEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/cnab/upload", async (IFormFile? request, ICommandHandler<UploadCommand> sender) =>
        {
            var summary = new ImportSummary();

            if (request is null || request.Length == 0)
            {
                return Results.BadRequest("File is required.");
            }

            using var reader = new StreamReader(request.OpenReadStream());
            string? line;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                summary.TotalLines++;
                var parser = new ParseUploadedFile(line);
                var command = parser.Parse();

                if (command.IsFailure)
                {
                    summary.ParseErrors++;
                    continue;
                }

                var result = await sender.HandleAsync(command.Value);

                if (result.IsSuccess)
                {
                    summary.ImportedTransactions++;
                }
            }

            return Results.Ok(new { Summary = summary });
        }).DisableAntiforgery();
    }
}
