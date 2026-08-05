using BuildingBlocks;
using BuildingBlocks.Extensions;
using Inventory.Application.Data;
using Inventory.Application.Exceptions;
using Inventory.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Handlers.BookInventories.Commands.Delete;

public class DeleteBookInventoryHandler(
    ILogger<DeleteBookInventoryHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<DeleteBookInventoryCommand, DeleteBookInventoryResult>
{
    public async Task<DeleteBookInventoryResult> Handle(DeleteBookInventoryCommand request, CancellationToken cancellationToken)
    {
        var bookInventoryId = new BookInventoryId(request.bookInventoryId);
        var bookInventory = await context.BookInventories.FindAsync([bookInventoryId], cancellationToken: cancellationToken);
        if (bookInventory is null)
        {
            throw new BookInventoryNotFoundException(request.bookInventoryId);
        }
        context.BookInventories.Remove(bookInventory);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogDeleteInformation(request.bookInventoryId);
        return new DeleteBookInventoryResult(null);
    }
}