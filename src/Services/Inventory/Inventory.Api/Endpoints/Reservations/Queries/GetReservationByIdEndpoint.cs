using FastEndpoints;
using Inventory.Application.Handlers.Reservations.Queries.GetById;

namespace Catalog.Api.Endpoints.Reservations.Queries;

public class GetReservationByIdEndpoint : Endpoint<GetReservationByIdQuery, GetReservationByIdResult>
{
    private readonly GetReservationByIdHandler _handler;

    public GetReservationByIdEndpoint(GetReservationByIdHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/reservation/{{reservationId}}");

        Description(x =>
        {
            x.WithTags("Reservation");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetReservationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}