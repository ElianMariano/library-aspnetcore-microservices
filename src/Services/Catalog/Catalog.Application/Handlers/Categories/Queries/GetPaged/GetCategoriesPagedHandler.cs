using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Dtos;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Handlers.Categories.Queries.GetPaged;

public class GetCategoriesPagedHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetCategoriesPagedQuery, GetCategoriesPagedResult>
{
    public async Task<GetCategoriesPagedResult> Handle(GetCategoriesPagedQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Category> query = context.Categories.AsNoTracking();
        int totalItems = await query.CountAsync(cancellationToken);
        var categories = await query
            .Skip((request.CurrentPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var data = categories.Select(category => new CategoryDto(
            category.Id.Value,
            category.Name)).ToList();
        return new GetCategoriesPagedResult(data, totalItems, request.CurrentPage, request.PageSize);
    }
}