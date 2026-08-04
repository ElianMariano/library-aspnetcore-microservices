using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.Entities;

public class BookInventory
{
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
}