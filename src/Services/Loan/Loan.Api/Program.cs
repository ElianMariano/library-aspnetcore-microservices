using FastEndpoints;
using FastEndpoints.Swagger;
using Loan.Infrastructure;
using Loan.Application;
using Loan.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints();

builder.Services.SwaggerDocument(options =>
{
    options.DocumentSettings = settings =>
    {
        settings.Title = "Loan API";
        settings.Version = "v1";
        settings.Description = "Catalog API for managing Loan Registries.";
    };
    options.AutoTagPathSegmentIndex = 0;
});

builder.Services.AddGrpc();
builder.Services.ConfigureGrpc(builder.Configuration);
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