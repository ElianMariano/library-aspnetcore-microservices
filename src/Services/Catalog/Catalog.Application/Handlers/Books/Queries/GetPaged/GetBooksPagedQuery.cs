using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Dtos;

namespace Catalog.Application.Handlers.Books.Queries.GetPaged;

public sealed class GetBooksPagedQuery(int currentPage = 1, int pageSize = 12) : PagedRequestBase(currentPage, pageSize);

public sealed class GetBooksPagedResult(IReadOnlyList<BookDto> Data, int TotalItems, int currentPage = 1, int pageSize = 12) : PagedResponseBase<BookDto>(Data, TotalItems, currentPage, pageSize);