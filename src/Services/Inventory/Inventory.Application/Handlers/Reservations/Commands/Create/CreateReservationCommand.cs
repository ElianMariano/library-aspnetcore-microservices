using BuildingBlocks.DataTransferObjects;
using FluentValidation;
using Inventory.Application.Dtos;
using Inventory.Application.Rules;

namespace Inventory.Application.Handlers.Reservations.Commands.Create;

public record CreateReservationCommand(ReservationDto reservation);

public class CreateReservationResult(Guid Id) : ResponseBase<Guid?>(Id);

public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationValidator()
    {
        RuleFor(x => x.reservation.bookId).BookId();

        RuleFor(x => x.reservation.userId).AuthorId();
    }
}