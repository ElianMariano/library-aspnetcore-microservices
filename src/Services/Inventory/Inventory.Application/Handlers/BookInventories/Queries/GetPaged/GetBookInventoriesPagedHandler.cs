using BuildingBlocks;
using Inventory.Application.Data;
using Inventory.Application.Dtos;
using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.Handlers.BookInventories.Queries.GetPaged;

public class GetBookInventoriesPagedHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetBookInventoriesPagedQuery, GetBookInventoriesPagedResult>
{
    public async Task<GetBookInventoriesPagedResult> Handle(GetBookInventoriesPagedQuery request, CancellationToken cancellationToken)
    {
        IQueryable<BookInventory> query = context.BookInventories.AsNoTracking();
        int totalItems = await query.CountAsync(cancellationToken);
        var bookInventories = await query
            .Skip((request.CurrentPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var data = bookInventories.Select(bookInventory => new BookInventoryDto(
            bookInventory.Id.Value,
            bookInventory.BookId,
            bookInventory.TotalCopies,
            bookInventory.AvailableCopies,
            bookInventory.ReservedCopies)).ToList();
        return new GetBookInventoriesPagedResult(data, totalItems, request.CurrentPage, request.PageSize);
    }
}