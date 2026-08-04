namespace Catalog.Application.Exceptions;

public sealed class AuthorNotFoundException : ApplicationException
{
    public AuthorNotFoundException(Guid authorId) : base("Author not found", authorId)
    {
    }
}