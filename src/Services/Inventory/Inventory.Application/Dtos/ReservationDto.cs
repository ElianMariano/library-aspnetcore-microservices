namespace Inventory.Application.Dtos;

public record ReservationDto(Guid reservationId, Guid bookId, Guid authorId, int quantity, DateTime expiresAt);