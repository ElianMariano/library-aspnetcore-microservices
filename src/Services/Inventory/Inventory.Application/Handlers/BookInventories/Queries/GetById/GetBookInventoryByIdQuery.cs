using BuildingBlocks.DataTransferObjects;
using Inventory.Application.Dtos;

namespace Inventory.Application.Handlers.BookInventories.Queries.GetById;

public record GetBookInventoryByIdQuery(Guid bookInventoryId);

public sealed class GetBookInventoryByIdResult(BookInventoryDto Data, int StatusCode = 200) : ResponseBase<BookInventoryDto>(Data, StatusCode);