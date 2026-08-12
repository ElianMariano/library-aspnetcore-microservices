using Loan.Application.Data;
using Loan.Application.Services;
using Loan.Infrastructure.Persistence;
using Loan.Infrastructure.Services;
using Member.Grpc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Stock.Grpc;
using System.Data;

namespace Loan.Infrastructure;

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
        builder.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        builder.AddScoped<IMembershipService, MembershipService>();
        builder.AddScoped<IInventoryService, InventoryService>();
    }

    public static void ConfigureGrpc(this IServiceCollection builder, IConfiguration configuration)
    {
        builder.AddGrpcClient<StockService.StockServiceClient>(options =>
        {
            options.Address = new Uri(configuration["GrcpStockServer"]!);
        });
        builder.AddGrpcClient<MemberService.MemberServiceClient>(options =>
        {
            options.Address = new Uri(configuration["GrcpMemberServer"]!);
        });
    }
}