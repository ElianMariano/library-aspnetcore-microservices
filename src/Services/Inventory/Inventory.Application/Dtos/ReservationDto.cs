namespace Inventory.Application.Dtos;

public record ReservationDto(Guid reservationId, Guid bookId, Guid userId, int quantity, DateOnly expiresAt);