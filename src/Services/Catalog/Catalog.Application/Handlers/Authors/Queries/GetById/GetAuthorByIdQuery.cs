using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Dtos;

namespace Catalog.Application.Handlers.Authors.Queries.GetById;

public record GetAuthorByIdQuery(Guid authorId);

public sealed class GetAuthorByIdResult(AuthorDto Data, int StatusCode = 200) : ResponseBase<AuthorDto>(Data, StatusCode);