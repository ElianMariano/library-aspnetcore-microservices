using Membership.Application.Handlers.Members;
using Microsoft.Extensions.DependencyInjection;

namespace Membership.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection builder)
    {
        IncludeMemberHandlers.Include(builder);
    }
}