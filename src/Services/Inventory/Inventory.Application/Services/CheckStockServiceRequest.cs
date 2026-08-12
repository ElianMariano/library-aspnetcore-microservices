namespace Inventory.Application.Services;

public record CheckStockServiceRequest(Guid bookId, int quantity);