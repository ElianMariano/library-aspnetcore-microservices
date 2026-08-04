using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Extensions;
using Catalog.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers.Categories.Commands.Create;

public class CreateCategoryHandler(
    ILogger<CreateCategoryHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<CreateCategoryCommand, CreateCategoryResult>
{
    public async Task<CreateCategoryResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.name);
        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogCreateInformation(category.Id!.Value);
        return new CreateCategoryResult(category.Id!.Value);
    }
}