using Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    public static void Configuration(this IServiceCollection builder, string connectionString)
    {
        builder.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        builder.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));
        //builder.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}