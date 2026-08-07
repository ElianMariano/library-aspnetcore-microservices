using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Dtos;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Handlers.Books.Queries.GetPaged;

public class GetBooksPagedHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetBooksPagedQuery, GetBooksPagedResult>
{
    public async Task<GetBooksPagedResult> Handle(GetBooksPagedQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Book> query = context.Books.AsNoTracking();
        int totalItems = await query.CountAsync(cancellationToken);
        var books = await query
            .Skip((request.CurrentPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var data = books.Select(book => new BookDto(
            book.Id.Value,
            book.Title,
            book.Isbn,
            book.PublicationYear,
            book.AuthorId.Value,
            book.CategoryId.Value)).ToList();
        return new GetBooksPagedResult(data, totalItems, request.CurrentPage, request.PageSize);
    }
}