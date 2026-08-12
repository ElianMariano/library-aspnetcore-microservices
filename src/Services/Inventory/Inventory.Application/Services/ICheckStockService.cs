namespace Inventory.Application.Services;

public interface ICheckStockService
{
    Task<CheckStockServiceResponse> CheckStockAsync(CheckStockServiceRequest request);
}