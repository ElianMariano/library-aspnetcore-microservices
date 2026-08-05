using FastEndpoints;
using Inventory.Application.Handlers.Reservations.Commands.Delete;

namespace Catalog.Api.Endpoints.Reservations.Commands;

public class DeleteReservationEndpoint : Endpoint<DeleteReservationCommand, DeleteReservationResult>
{
    private readonly DeleteReservationHandler _handler;

    public DeleteReservationEndpoint(DeleteReservationHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Delete($"/reservation/{{itemId}}");

        Description(x =>
        {
            x.WithTags("Reservation");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteReservationCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}