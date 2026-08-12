using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.Entities;

public class Reservation
{
    public ReservationId Id { get; private init; }

    public Guid BookId { get; set; }

    public Guid UserId { get; set; }

    public int Quantity { get; set; }

    public DateOnly ExpiresAt { get; set; }

    public Reservation(
        Guid bookId,
        Guid userId,
        int quantity,
        DateOnly expiresAt)
    {
        Id = new ReservationId(Guid.NewGuid());
        BookId = bookId;
        UserId = userId;
        Quantity = quantity;
        ExpiresAt = expiresAt;
    }
}