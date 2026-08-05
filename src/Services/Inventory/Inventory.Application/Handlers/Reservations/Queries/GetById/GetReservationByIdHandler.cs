using BuildingBlocks;
using Inventory.Application.Data;
using Inventory.Application.Dtos;
using Inventory.Application.Exceptions;
using Inventory.Domain.ValueObjects;

namespace Inventory.Application.Handlers.Reservations.Queries.GetById;

public class GetReservationByIdHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetReservationByIdQuery, GetReservationByIdResult>
{
    public async Task<GetReservationByIdResult> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var reservationId = new ReservationId(request.reservationId);
        var reservation = await context.Reservations.FindAsync([reservationId], cancellationToken: cancellationToken);
        if (reservation is null)
        {
            throw new ReservationNotFoundException(request.reservationId);
        }
        var data = new ReservationDto(
            reservation.Id.Value,
            reservation.BookId,
            reservation.UserId,
            reservation.Quantity,
            reservation.ExpiresAt);
        return new GetReservationByIdResult(data);
    }
}