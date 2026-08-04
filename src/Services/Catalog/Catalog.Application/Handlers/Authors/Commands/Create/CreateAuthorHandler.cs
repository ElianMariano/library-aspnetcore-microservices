using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Extensions;
using Catalog.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers.Authors.Commands.Create;

public class CreateAuthorHandler(
    ILogger<CreateAuthorHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<CreateAuthorCommand, CreateAuthorResult>
{
    public async Task<CreateAuthorResult> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author(request.name);
        await context.Authors.AddAsync(author, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogCreateInformation(author.Id!.Value);
        return new CreateAuthorResult(author.Id!.Value);
    }
}