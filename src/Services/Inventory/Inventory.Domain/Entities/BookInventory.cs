using Inventory.Domain.Exceptions;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.Entities;

public class BookInventory
{
    // TODO: It would be interesting to use only bookId as the primary key, but for learning purposes, let's keep the Id separate.
    public BookInventoryId Id { get; private init; }

    public Guid BookId { get; set; }

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }

    public int ReservedCopies { get; set; }

    public BookInventory(
        Guid bookId,
        int totalCopies,
        int availableCopies,
        int reservedCopies)
    {
        Id = new BookInventoryId(Guid.NewGuid());
        BookId = bookId;
        TotalCopies = totalCopies;
        AvailableCopies = availableCopies;
        ReservedCopies = reservedCopies;
    }

    public void ReserveCopies()
    {
        // TODO: Right now we are reserving only 1 copy, but we can change this to reserve more copies if needed.
        int quantityToReserve = 1;
        if (AvailableCopies < quantityToReserve)
        {
            throw new UnavalilableCopiesToReserveException();
        }
        AvailableCopies -= quantityToReserve;
        ReservedCopies += quantityToReserve;
    }

    public void ReturnCopies()
    {
        int quantityToReturn = 1;
        AvailableCopies += quantityToReturn;
        ReservedCopies -= quantityToReturn;
    }

}