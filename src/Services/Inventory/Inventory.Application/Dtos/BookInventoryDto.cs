namespace Inventory.Application.Dtos;

public record BookInventoryDto(Guid bookInventoryId, Guid bookId, int totalCopies, int availableCopies, int reservedCopies);