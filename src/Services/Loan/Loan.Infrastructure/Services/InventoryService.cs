using Loan.Application.Services;
using Stock.Grpc;

namespace Loan.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly StockService.StockServiceClient _client;

    public InventoryService(StockService.StockServiceClient client)
    {
        _client = client;
    }

    public async Task<InventoryServiceResponse> CheckStock(InventoryServiceRequest request)
    {
        var response = await _client.CheckStockAsync(new CheckStockRequest { BookId = request.bookId.ToString(), Quantity = request.quantity });
        return new InventoryServiceResponse(response.Available, response.AvailableQuantity);
    }
}