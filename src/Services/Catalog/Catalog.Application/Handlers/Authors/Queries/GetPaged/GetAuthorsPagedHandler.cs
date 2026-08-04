using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Dtos;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Handlers.Authors.Queries.GetPaged;

public class GetAuthorsPagedHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetAuthorsPagedQuery, GetAuthorsPagedResult>
{
    public async Task<GetAuthorsPagedResult> Handle(GetAuthorsPagedQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Author> query = context.Authors.AsNoTracking();
        int totalItems = await query.CountAsync(cancellationToken);
        var authors = await query
            .Skip((request.CurrentPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var data = authors.Select(author => new AuthorDto(
            author.Id.Value,
            author.Name)).ToList();
        return new GetAuthorsPagedResult(data, totalItems, request.CurrentPage, request.PageSize);
    }
}