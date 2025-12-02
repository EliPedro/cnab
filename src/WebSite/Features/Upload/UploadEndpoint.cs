using Carter;
using WebSite.Shared;

namespace WebSite.Features.Upload;

public class UploadEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/cnab/upload", async (IFormFile? request, ICommandHandler<UploadCommand> sender) =>
        {
            if (request is null || request.Length == 0)
            {
                return Results.BadRequest("File is required.");
            }

            using var reader = new StreamReader(request.OpenReadStream());
            string? line;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                var parser = new ParseUploadedFile(line);
                var command = parser.Parse();
                var result = await sender.Handle(command);
            }

            return Results.Created();
        });
    }
}
