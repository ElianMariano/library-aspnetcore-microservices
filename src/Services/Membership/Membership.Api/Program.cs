using FastEndpoints;
using FastEndpoints.Swagger;
using Membership.Infrastructure;
using Membership.Application;
using Membership.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints();

builder.Services.SwaggerDocument(options =>
{
    options.DocumentSettings = settings =>
    {
        settings.Title = "Membership API";
        settings.Version = "v1";
        settings.Description = "Membership API for managing Members.";
    };
    options.AutoTagPathSegmentIndex = 0;
});

builder.Services.Configuration(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddApplicationServices();
builder.Services.AddExceptionHandler<ExceptionMiddleware>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseRequestLocalization();

app.UseExceptionHandler();
app.UseFastEndpoints();

app.UseOpenApi();
app.UseSwaggerUi();

app.Run();