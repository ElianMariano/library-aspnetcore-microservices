using Inventory.Application.Data;
using Inventory.Application.Services;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;
using System.Reflection;
using BuildingBlocks.Messaging.MassTransit;

namespace Inventory.Infrastructure;

public static class DependencyInjection
{
    public static async Task InitialiseDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();
    }

    public static void Configuration(this IServiceCollection builder, string connectionString)
    {
        builder.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        builder.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));
        builder.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        builder.AddScoped<ICheckStockService, CheckStockService>();
    }

    public static void BrokerConfig(this IServiceCollection builder, IConfiguration configuration, Assembly? assembly = null)
    {
        builder.AddMessageBroker<ApplicationDbContext>(configuration, assembly);
    }
}