using BuildingBlocks;
using Catalog.Application.Data;
using Catalog.Application.Dtos;
using Catalog.Application.Exceptions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Handlers.Authors.Queries.GetById;

public class GetAuthorByIdHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetAuthorByIdQuery, GetAuthorByIdResult>
{
    public async Task<GetAuthorByIdResult> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var authorId = new AuthorId(request.authorId);
        var author = await context.Authors.FindAsync([authorId], cancellationToken: cancellationToken);
        if (author is null)
        {
            throw new BookNotFoundException(request.authorId);
        }
        var data = new AuthorDto(
            author.Id.Value,
            author.Name);
        return new GetAuthorByIdResult(data);
    }
}