using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Dtos;

namespace Catalog.Application.Handlers.Authors.Queries.GetPaged;

public sealed class GetAuthorsPagedQuery(int currentPage = 1, int pageSize = 12) : PagedRequestBase(currentPage, pageSize);

public sealed class GetAuthorsPagedResult(IReadOnlyList<AuthorDto> Data, int TotalItems, int currentPage = 1, int pageSize = 12) : PagedResponseBase<AuthorDto>(Data, TotalItems, currentPage, pageSize);