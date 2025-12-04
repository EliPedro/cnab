using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using WebSite.Components;
using WebSite.Database;
using WebSite.Extensions;
using WebSite.Features.Store;
using WebSite.Features.Upload;
using WebSite.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(sp =>
{
    var nav = sp.GetRequiredService<NavigationManager>();
    var env = sp.GetRequiredService<IWebHostEnvironment>();

    var baseUri = new Uri(nav.BaseUri);
    if (env.IsDevelopment())
    {
        var authority = baseUri.GetLeftPart(UriPartial.Authority).Replace("https:", "http:");
        baseUri = new Uri(authority + "/");
    }

    var client = new HttpClient { BaseAddress = baseUri };
    return client;
});


builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

var assembly = typeof(Program).Assembly;

builder.Services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
builder.Services.AddScoped<ICommandHandler<UploadCommand>, UploadHandler>();
builder.Services.AddScoped<IQueryCommandWithoudParams<StoreResponse>, StoreQuery>();

builder.Services.AddCarter();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();
app.MapCarter();
app.Run();
