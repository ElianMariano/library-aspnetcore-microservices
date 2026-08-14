using BuildingBlocks;
using BuildingBlocks.Extensions;
using Inventory.Application.Data;
using Inventory.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Handlers.BookInventories.Commands.Create;

public class CreateBookInventoryHandler(
    ILogger<CreateBookInventoryHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<CreateBookInventoryCommand, CreateBookInventoryResult>
{
    public async Task<CreateBookInventoryResult> Handle(CreateBookInventoryCommand request, CancellationToken cancellationToken)
    {
        var bookInventory = new BookInventory(
            request.bookInventory.bookId,
            request.bookInventory.availableCopies,
            request.bookInventory.reservedCopies);
        await context.BookInventories.AddAsync(bookInventory, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogCreateInformation(bookInventory.Id!.Value);
        return new CreateBookInventoryResult(bookInventory.Id!.Value);
    }
}