using Inventory.Application.Data;
using Inventory.Application.Services;

namespace Inventory.Infrastructure.Services;

public class CheckStockService(IApplicationDbContext context) : ICheckStockService
{
    public async Task<CheckStockServiceResponse> CheckStockAsync(CheckStockServiceRequest request)
    {
        var book = context.BookInventories.FirstOrDefault(x => x.BookId == request.bookId);
        return new CheckStockServiceResponse(book!.AvailableCopies >= request.quantity, book!.AvailableCopies);
    }
}