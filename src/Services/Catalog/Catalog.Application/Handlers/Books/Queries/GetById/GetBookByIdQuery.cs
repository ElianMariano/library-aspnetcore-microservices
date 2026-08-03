using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Dtos;

namespace Catalog.Application.Handlers.Books.Queries.GetById;

public record GetBookByIdQuery(Guid bookId);

public sealed class GetBookByIdResult(BookDto Data, int StatusCode = 200) : ResponseBase<BookDto>(Data, StatusCode);