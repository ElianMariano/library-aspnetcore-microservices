using Membership.Application.Handlers.Members;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;

namespace Membership.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection builder)
    {
        IncludeMemberHandlers.Include(builder);
    }

    public static void BrokerConfig(this IServiceCollection builder, IConfiguration configuration)
    {
        builder.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
    }
}