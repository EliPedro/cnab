using Carter;
using EP.AspireApp.Cnab.AppHost.Features.Cnab;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EP.AspireApp.Cnab.AppHost.Features.Upload;

public class UploadEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/cnab/upload", async (IFormFile? request, ISender sender) =>
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
                var result = await sender.Send(command);
            }

            return Results.Created();
        });
    }
}
