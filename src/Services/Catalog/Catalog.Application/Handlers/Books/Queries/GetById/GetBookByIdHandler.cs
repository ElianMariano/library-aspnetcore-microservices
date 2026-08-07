using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Dtos;
using Catalog.Application.Exceptions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Handlers.Books.Queries.GetById;

public class GetBookByIdHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetBookByIdQuery, GetBookByIdResult>
{
    public async Task<GetBookByIdResult> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var bookId = new BookId(request.bookId);
        var book = await context.Books.FindAsync([bookId], cancellationToken: cancellationToken);
        if (book is null)
        {
            throw new BookNotFoundException(request.bookId);
        }
        var data = new BookDto(
            book.Id.Value,
            book.Title,
            book.Isbn,
            book.PublicationYear,
            book.AuthorId.Value,
            book.CategoryId.Value
        );
        return new GetBookByIdResult(data);
    }
}