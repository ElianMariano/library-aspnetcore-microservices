using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Dtos;

namespace Catalog.Application.Handlers.Categories.Queries.GetPaged;

public sealed class GetCategoriesPagedQuery(int currentPage = 1, int pageSize = 12) : PagedRequestBase(currentPage, pageSize);

public sealed class GetCategoriesPagedResult(IReadOnlyList<CategoryDto> Data, int TotalItems, int currentPage = 1, int pageSize = 12) : PagedResponseBase<CategoryDto>(Data, TotalItems, currentPage, pageSize);