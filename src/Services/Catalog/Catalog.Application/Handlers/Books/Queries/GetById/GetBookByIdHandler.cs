using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Dtos;
using Catalog.Domain.Entities;
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
            throw new Exception(nameof(Book));
        }
        var data = new BookDto(
            book.Id.Value,
            book.Title,
            book.ISBN,
            book.PublicationYear,
            book.AuthorId.Value,
            book.CategoryId.Value
        );
        await context.Books.AddAsync(book, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return new GetBookByIdResult(data);
    }
}