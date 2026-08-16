using Inventory.Application.Handlers.BookInventories;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection builder)
    {
        IncludeBookInventoryHandlers.Include(builder);
    }
}