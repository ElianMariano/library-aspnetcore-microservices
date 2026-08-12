namespace Loan.Application.Services;

public interface IInventoryService
{
    Task<InventoryServiceResponse> CheckStock(InventoryServiceRequest request);
}