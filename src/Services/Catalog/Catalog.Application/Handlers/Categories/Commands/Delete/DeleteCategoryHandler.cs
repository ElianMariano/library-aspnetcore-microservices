using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Extensions;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers.Categories.Commands.Delete;

public class DeleteCategoryHandler(
    ILogger<DeleteCategoryHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<DeleteCategoryCommand, DeleteCategoryResult>
{
    public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryId = new CategoryId(request.categoryId);
        var category = await context.Categories.FindAsync([categoryId], cancellationToken: cancellationToken);
        if (category is null)
        {
            throw new Exception(nameof(Category));
        }
        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogDeleteInformation(request.categoryId);
        return new DeleteCategoryResult(null);
    }
}