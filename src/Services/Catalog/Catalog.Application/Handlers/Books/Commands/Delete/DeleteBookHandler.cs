using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Exceptions;
using Catalog.Application.Extensions;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers.Books.Commands.Delete;

public class DeleteBookHandler(
    ILogger<DeleteBookHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<DeleteBookCommand, DeleteBookResult>
{
    public async Task<DeleteBookResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var bookId = new BookId(request.bookId);
        var book = await context.Books.FindAsync([bookId], cancellationToken: cancellationToken);
        if (book is null)
        {
            throw new BookNotFoundException(request.bookId);
        }
        context.Books.Remove(book);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogDeleteInformation(request.bookId);
        return new DeleteBookResult(null);
    }
}