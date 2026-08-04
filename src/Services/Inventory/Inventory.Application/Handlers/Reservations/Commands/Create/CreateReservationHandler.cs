using BuildingBlocks;
using BuildingBlocks.Extensions;
using Inventory.Application.Data;
using Inventory.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Handlers.Reservations.Commands.Create;

public class CreateReservationHandler(
    ILogger<CreateReservationHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<CreateReservationCommand, CreateReservationResult>
{
    public async Task<CreateReservationResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = new Reservation(
            request.reservation.bookId,
            request.reservation.userId,
            request.reservation.quantity,
            request.reservation.expiresAt);
        await context.Reservations.AddAsync(reservation, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogCreateInformation(reservation.Id!.Value);
        return new CreateReservationResult(reservation.Id!.Value);
    }
}