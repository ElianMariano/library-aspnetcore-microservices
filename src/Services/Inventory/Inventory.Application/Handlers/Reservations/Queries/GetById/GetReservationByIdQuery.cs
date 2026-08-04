using BuildingBlocks.DataTransferObjects;
using Inventory.Application.Dtos;

namespace Inventory.Application.Handlers.Reservations.Queries.GetById;

public record GetReservationByIdQuery(Guid reservationId);

public sealed class GetReservationByIdResult(ReservationDto Data, int StatusCode = 200) : ResponseBase<ReservationDto>(Data, StatusCode);