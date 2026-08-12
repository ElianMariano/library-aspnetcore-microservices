using FastEndpoints;
using FastEndpoints.Swagger;
using Inventory.Api.Middlewares;
using Inventory.Infrastructure;
using Inventory.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints();

builder.Services.SwaggerDocument(options =>
{
    options.DocumentSettings = settings =>
    {
        settings.Title = "Inventory API";
        settings.Version = "v1";
        settings.Description = "Inventory API for managing BookInventories and Reservations.";
    };
    options.AutoTagPathSegmentIndex = 0;
});

builder.Services.Configuration(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddApplicationServices();
builder.Services.BrokerConfig(builder.Configuration);
builder.Services.AddExceptionHandler<ExceptionMiddleware>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseRequestLocalization();

app.UseExceptionHandler();
app.UseFastEndpoints();

app.UseOpenApi();
app.UseSwaggerUi();

if (app.Environment.IsDevelopment())
{
    await app.Services.InitialiseDatabaseAsync();
}

app.Run();