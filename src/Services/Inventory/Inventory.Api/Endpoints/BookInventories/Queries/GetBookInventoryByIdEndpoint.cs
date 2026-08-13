using FastEndpoints;
using Inventory.Application.Handlers.BookInventories.Queries.GetById;

namespace Inventory.Api.Endpoints.BookInventories.Queries;

public class GetBookInventoryByIdEndpoint : Endpoint<GetBookInventoryByIdQuery, GetBookInventoryByIdResult>
{
    private readonly GetBookInventoryByIdHandler _handler;

    public GetBookInventoryByIdEndpoint(GetBookInventoryByIdHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/bookinventory/{{bookInventoryId}}");

        Description(x =>
        {
            x.WithTags("BookInventory");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetBookInventoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}