using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Dtos;
using Catalog.Application.Exceptions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Handlers.Categories.Queries.GetById;

public class GetCategoryByIdHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetCategoryByIdQuery, GetCategoryByIdResult>
{
    public async Task<GetCategoryByIdResult> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryId = new CategoryId(request.categoryId);
        var category = await context.Categories.FindAsync([categoryId], cancellationToken: cancellationToken);
        if (category is null)
        {
            throw new CategoryNotFoundException(request.categoryId);
        }
        var data = new CategoryDto(
            category.Id.Value,
            category.Name);
        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return new GetCategoryByIdResult(data);
    }
}