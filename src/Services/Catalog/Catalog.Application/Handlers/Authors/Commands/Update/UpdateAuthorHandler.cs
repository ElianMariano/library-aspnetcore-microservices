using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Extensions;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers.Authors.Commands.Update;

public class UpdateAuthorHandler(
    ILogger<UpdateAuthorHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<UpdateAuthorCommand, UpdateAuthorResult>
{
    public async Task<UpdateAuthorResult> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorId = new AuthorId(request.author.authorId);
        var author = await context.Authors.FindAsync([authorId], cancellationToken: cancellationToken);
        if (author is null)
        {
            throw new Exception(nameof(Author));
        }
        author.Update(request.author.name);
        context.Authors.Update(author);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogUpdateInformation(authorId.Value);
        return new UpdateAuthorResult(authorId.Value);
    }
}