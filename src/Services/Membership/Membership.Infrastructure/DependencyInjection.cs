using Membership.Application.Data;
using Membership.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace Membership.Infrastructure;

public static class DependencyInjection
{
    public static void Configuration(this IServiceCollection builder, string connectionString)
    {
        builder.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        builder.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));
        builder.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }
}