using BuildingBlocks.DataTransferObjects;
using FluentValidation;
using Inventory.Application.Rules;

namespace Inventory.Application.Handlers.Reservations.Commands.Delete;

public record DeleteReservationCommand(Guid reservationId);

public class DeleteReservationResult(Guid? Id) : ResponseBase<Guid?>(Id);

public class DeleteReservationValidator : AbstractValidator<DeleteReservationCommand>
{
    public DeleteReservationValidator()
    {
        RuleFor(x => x.reservationId).ReservationId();
    }
}