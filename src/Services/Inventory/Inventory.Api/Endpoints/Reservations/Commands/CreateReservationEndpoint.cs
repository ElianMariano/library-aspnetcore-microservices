using FastEndpoints;
using Inventory.Application.Handlers.Reservations.Commands.Create;

namespace Catalog.Api.Endpoints.Reservations.Commands;

public class CreateReservationEndpoint : Endpoint<CreateReservationCommand, CreateReservationResult>
{
    private readonly CreateReservationHandler _handler;

    public CreateReservationEndpoint(CreateReservationHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Post("/reservation");

        Description(x =>
        {
            x.WithTags("Reservation");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}