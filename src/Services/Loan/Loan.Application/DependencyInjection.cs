using Loan.Application.Handlers.LoanRegistries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BuildingBlocks.Messaging.MassTransit;

namespace Loan.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection builder)
    {
        IncludeLoanRegistryHandlers.Include(builder);
    }

    public static void BrokerConfig(this IServiceCollection builder, IConfiguration configuration)
    {
        builder.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
    }
}