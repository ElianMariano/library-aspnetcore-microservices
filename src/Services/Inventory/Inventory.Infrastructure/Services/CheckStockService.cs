using Inventory.Application.Data;
using Inventory.Application.Services;

namespace Inventory.Infrastructure.Services;

public class CheckStockService(IApplicationDbContext context) : ICheckStockService
{
    public async Task<CheckStockServiceResponse> CheckStockAsync(CheckStockServiceRequest request)
    {
        var book = context.BookInventories.FirstOrDefault(x => x.BookId == request.bookId);
        if (book  == null)
        {
            return new CheckStockServiceResponse(false, 0);
        }
        return new CheckStockServiceResponse(book!.AvailableCopies >= request.quantity, book!.AvailableCopies);
    }
}