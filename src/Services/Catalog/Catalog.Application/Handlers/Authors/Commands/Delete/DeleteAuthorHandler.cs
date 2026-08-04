using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Extensions;
using Catalog.Application.Handlers.Authors.Commands.Delete;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Handlers.Authors.Commands.Delete;

public class DeleteAuthorHandler(
    ILogger<DeleteAuthorHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<DeleteAuthorCommand, DeleteAuthorResult>
{
    public async Task<DeleteAuthorResult> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorId = new AuthorId(request.authorId);
        var author = await context.Authors.FindAsync([authorId], cancellationToken: cancellationToken);
        if (author is null)
        {
            throw new Exception(nameof(Author));
        }
        context.Authors.Remove(author);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogDeleteInformation(request.authorId);
        return new DeleteAuthorResult(null);
    }
}