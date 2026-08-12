using Grpc.Core;
using Inventory.Application.Services;
using Stock.Grpc;

namespace Inventory.Api.Services;

public class StockGrpcService(ICheckStockService checkStockService) : StockService.StockServiceBase
{
    public override async Task<CheckStockResponse> CheckStock(CheckStockRequest request, ServerCallContext context)
    {
        var response = await checkStockService.CheckStockAsync(new CheckStockServiceRequest(Guid.Parse(request.BookId), request.Quantity));
        return new CheckStockResponse
        {
            Available = response.available,
            AvailableQuantity = response.availableQuantity
        };
    }
}