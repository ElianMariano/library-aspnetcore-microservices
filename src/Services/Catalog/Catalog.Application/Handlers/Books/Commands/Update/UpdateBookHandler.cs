using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Extensions;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers.Books.Commands.Update;

public class UpdateBookHandler(
    ILogger<UpdateBookHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<UpdateBookCommand, UpdateBookResult>
{
    public async Task<UpdateBookResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var bookId = new BookId(request.book.id);
        var book = await context.Books.FindAsync([bookId], cancellationToken: cancellationToken);
        if (book is null)
        {
            throw new Exception(nameof(Book));
        }
        book.Update(
            request.book.title,
            request.book.isbn,
            request.book.publicationYear);
        context.Books.Update(book);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogUpdateInformation(request.book.id);
        return new UpdateBookResult(request.book.id);
    }
}