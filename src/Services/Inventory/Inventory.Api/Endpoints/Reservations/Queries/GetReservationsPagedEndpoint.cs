using FastEndpoints;
using Inventory.Application.Handlers.Reservations.Queries.GetPaged;

namespace Catalog.Api.Endpoints.Reservations.Queries;

public class GetReservationsPagedEndpoint : Endpoint<GetReservationsPagedQuery, GetReservationsPagedResult>
{
    private readonly GetReservationsPagedHandler _handler;

    public GetReservationsPagedEndpoint(GetReservationsPagedHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/reservations");

        Description(x =>
        {
            x.WithTags("Reservation");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetReservationsPagedQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}