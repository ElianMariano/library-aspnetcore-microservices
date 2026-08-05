using Inventory.Application.Handlers.BookInventories.Commands.Create;
using Inventory.Application.Handlers.BookInventories.Commands.Delete;
using Inventory.Application.Handlers.BookInventories.Queries.GetById;
using Inventory.Application.Handlers.BookInventories.Queries.GetPaged;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application.Handlers.BookInventories;

public class IncludeBookInventoryHandlers
{
    public static void Include(IServiceCollection builder)
    {
        builder.AddScoped<CreateBookInventoryHandler>();
        builder.AddScoped<DeleteBookInventoryHandler>();
        builder.AddScoped<GetBookInventoryByIdHandler>();
        builder.AddScoped<GetBookInventoriesPagedHandler>();
    }
}