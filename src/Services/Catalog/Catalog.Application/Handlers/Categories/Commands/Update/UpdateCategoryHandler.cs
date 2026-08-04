using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Extensions;
using Catalog.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers.Categories.Commands.Update;

public class UpdateCategoryHandler(
    ILogger<UpdateCategoryHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<UpdateCategoryCommand, UpdateCategoryResult>
{
    public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryId = new CategoryId(request.category.categoryId);
        var category = await context.Categories.FindAsync([categoryId], cancellationToken: cancellationToken);
        if (category is null)
        {
            throw new Exception(nameof(category));
        }
        category.Update(request.category.name);
        context.Categories.Update(category);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogUpdateInformation(categoryId.Value);
        return new UpdateCategoryResult(categoryId.Value);
    }
}