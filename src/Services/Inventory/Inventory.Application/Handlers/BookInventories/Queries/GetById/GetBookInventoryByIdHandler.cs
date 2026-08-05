using BuildingBlocks;
using Inventory.Application.Data;
using Inventory.Application.Dtos;
using Inventory.Application.Exceptions;
using Inventory.Domain.ValueObjects;

namespace Inventory.Application.Handlers.BookInventories.Queries.GetById;

public class GetBookInventoryByIdHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetBookInventoryByIdQuery, GetBookInventoryByIdResult>
{
    public async Task<GetBookInventoryByIdResult> Handle(GetBookInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        var bookInventoryId = new BookInventoryId(request.bookInventoryId);
        var bookInventory = await context.BookInventories.FindAsync([bookInventoryId], cancellationToken: cancellationToken);
        if (bookInventory is null)
        {
            throw new BookInventoryNotFoundException(request.bookInventoryId);
        }
        var data = new BookInventoryDto(
            bookInventory.Id.Value,
            bookInventory.BookId,
            bookInventory.TotalCopies,
            bookInventory.AvailableCopies,
            bookInventory.ReservedCopies);
        await context.BookInventories.AddAsync(bookInventory, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return new GetBookInventoryByIdResult(data);
    }
}