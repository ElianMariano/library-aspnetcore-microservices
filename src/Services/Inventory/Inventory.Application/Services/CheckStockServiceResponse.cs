namespace Inventory.Application.Services;

public record CheckStockServiceResponse(bool available, int availableQuantity);