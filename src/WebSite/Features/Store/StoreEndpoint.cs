using Carter;
using WebSite.Shared;

namespace WebSite.Features.Store
{
    public class StoreEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/cnab/stores", async (IQueryCommandWithoudParams<StoreResponse> sender) =>
            {
                return Results.Ok(await sender.HandleAsync());
            });
        }
    }
}
