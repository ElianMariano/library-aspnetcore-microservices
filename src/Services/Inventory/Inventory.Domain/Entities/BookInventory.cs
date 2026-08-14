using Inventory.Domain.Exceptions;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.Entities;

public class BookInventory
{
    // NOTE: It would be interesting to use only bookId as the primary key, but for learning purposes, let's keep the Id separate.
    public BookInventoryId Id { get; private init; }

    public Guid BookId { get; set; }

    public int TotalCopies => this.AvailableCopies + this.ReservedCopies;

    public int AvailableCopies { get; set; }

    public int ReservedCopies { get; set; }

    public BookInventory(
        Guid bookId,
        int availableCopies,
        int reservedCopies)
    {
        Id = new BookInventoryId(Guid.NewGuid());
        BookId = bookId;
        AvailableCopies = availableCopies;
        ReservedCopies = reservedCopies;
    }

    public void ReserveCopies(int quantity)
    {
        if (AvailableCopies < quantity)
        {
            throw new UnavalilableCopiesToReserveException();
        }
        AvailableCopies -= quantity;
        ReservedCopies += quantity;
    }

    public void ReturnCopies(int quantity)
    {
        AvailableCopies += quantity;
        ReservedCopies -= quantity;
    }
}