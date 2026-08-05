using BuildingBlocks.DataTransferObjects;
using Inventory.Application.Dtos;

namespace Inventory.Application.Handlers.BookInventories.Queries.GetPaged;

public sealed class GetBookInventoriesPagedQuery(int currentPage = 1, int pageSize = 12) : PagedRequestBase(currentPage, pageSize);

public sealed class GetBookInventoriesPagedResult(IReadOnlyList<BookInventoryDto> Data, int TotalItems, int currentPage = 1, int pageSize = 12) : PagedResponseBase<BookInventoryDto>(Data, TotalItems, currentPage, pageSize);