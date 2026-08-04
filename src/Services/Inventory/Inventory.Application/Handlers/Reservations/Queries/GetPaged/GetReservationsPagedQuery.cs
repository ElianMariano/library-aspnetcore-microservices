using BuildingBlocks.DataTransferObjects;
using Inventory.Application.Dtos;

namespace Inventory.Application.Handlers.Reservations.Queries.GetPaged;

public sealed class GetReservationsPagedQuery(int currentPage = 1, int pageSize = 12) : PagedRequestBase(currentPage, pageSize);

public sealed class GetReservationsPagedResult(IReadOnlyList<ReservationDto> Data, int TotalItems, int currentPage = 1, int pageSize = 12) : PagedResponseBase<ReservationDto>(Data, TotalItems, currentPage, pageSize);