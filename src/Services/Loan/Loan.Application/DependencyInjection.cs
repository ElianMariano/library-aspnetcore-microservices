using Loan.Application.Handlers.LoanRegistries;
using Microsoft.Extensions.DependencyInjection;

namespace Loan.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection builder)
    {
        IncludeLoanRegistryHandlers.Include(builder);
    }
}