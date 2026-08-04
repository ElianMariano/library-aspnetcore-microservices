namespace Catalog.Application.Exceptions;

public sealed class BookNotFoundException : ApplicationException
{
    public BookNotFoundException(Guid bookId) : base("Book not found", bookId)
    {
    }
}