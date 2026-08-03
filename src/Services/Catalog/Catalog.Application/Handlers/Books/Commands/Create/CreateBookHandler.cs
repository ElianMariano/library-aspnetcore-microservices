using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Extensions;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers.Books.Commands.Create;

public class CreateBookHandler(
    ILogger<CreateBookHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<CreateBookCommand, CreateBookResult>
{
    public async Task<CreateBookResult> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book(
            request.book.title,
            request.book.isbn,
            request.book.publicationYear,
            new AuthorId(request.book.authorId),
            new CategoryId(request.book.categoryId));
        await context.Books.AddAsync(book, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogCreateInformation(request.book.id);
        return new CreateBookResult(request.book.id);
    }
}