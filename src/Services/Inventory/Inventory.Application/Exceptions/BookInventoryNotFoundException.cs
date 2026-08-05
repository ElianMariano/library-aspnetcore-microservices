namespace Inventory.Application.Exceptions;

public sealed class BookInventoryNotFoundException : ApplicationException
{
    public BookInventoryNotFoundException(Guid bookInventoryId) : base("Book Inventory not found", bookInventoryId)
    {
    }
}