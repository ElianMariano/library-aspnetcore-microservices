using BuildingBlocks;
using BuildingBlocks.Extensions;
using Inventory.Application.Data;
using Inventory.Application.Exceptions;
using Inventory.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Handlers.Reservations.Commands.Delete;

public class DeleteReservationHandler(
    ILogger<DeleteReservationHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<DeleteReservationCommand, DeleteReservationResult>
{
    public async Task<DeleteReservationResult> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        var reservationId = new ReservationId(request.reservationId);
        var reservation = await context.Reservations.FindAsync([reservationId], cancellationToken: cancellationToken);
        if (reservation is null)
        {
            throw new ReservationNotFoundException(request.reservationId);
        }
        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogDeleteInformation(request.reservationId);
        return new DeleteReservationResult(null);
    }
}