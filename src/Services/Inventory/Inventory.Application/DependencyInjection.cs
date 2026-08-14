using Inventory.Application.Handlers.BookInventories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BuildingBlocks.Messaging.MassTransit;

namespace Inventory.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection builder)
    {
        IncludeBookInventoryHandlers.Include(builder);
    }

    public static void BrokerConfig(this IServiceCollection builder, IConfiguration configuration)
    {
        builder.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
    }
}