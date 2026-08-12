namespace Loan.Application.Services;

public record InventoryServiceRequest(Guid bookId, int quantity);