using Loan.Domain.Exceptions;
using Loan.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Loan.Api.Middlewares;

public class ExceptionMiddleware : IExceptionHandler
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception.Message);

        httpContext.Response.ContentType = "application/json";

        var problem = exception switch
        {
            Application.Exceptions.ApplicationException ex => new ProblemDetails
            {
                Title = exception.Message,
                Detail = exception.GetType().Name,
                Status = StatusCodes.Status400BadRequest
            },
            InfrastructureException ex => new ProblemDetails
            {
                Title = exception.Message,
                Detail = exception.GetType().Name,
                Status = StatusCodes.Status400BadRequest
            },
            DomainException ex => new ProblemDetails
            {
                Title = exception.Message,
                Detail = exception.GetType().Name,
                Status = StatusCodes.Status400BadRequest
            },
            _ => new ProblemDetails
            {
                Title = exception.Message,
                Detail = exception.ToString(),
                Status = StatusCodes.Status500InternalServerError
            }
        };

        httpContext.Response.StatusCode = problem.Status!.Value;

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}