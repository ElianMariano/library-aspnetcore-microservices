using Loan.Application.Handlers.LoanRegistries.Commands.Create;
using Loan.Application.Handlers.LoanRegistries.Commands.Return;
using Loan.Application.Handlers.LoanRegistries.Queries.GetById;
using Loan.Application.Handlers.LoanRegistries.Queries.GetPaged;
using Microsoft.Extensions.DependencyInjection;

namespace Loan.Application.Handlers.LoanRegistries;

public static class IncludeLoanRegistryHandlers
{
    public static void Include(IServiceCollection builder)
    {
        builder.AddScoped<CreateLoanRegistryHandler>();
        builder.AddScoped<ReturnLoanRegistryHandler>();
        builder.AddScoped<GetLoanRegistryByIdHandler>();
        builder.AddScoped<GetLoanRegistriesPagedHandler>();
    }
}
