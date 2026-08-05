using FastEndpoints;
using Inventory.Application.Handlers.BookInventories.Queries.GetPaged;

namespace Inventory.Api.Endpoints.BookInventories.Queries;

public class GetBookInventoriesPagedEndpoint : Endpoint<GetBookInventoriesPagedQuery, GetBookInventoriesPagedResult>
{
    private readonly GetBookInventoriesPagedHandler _handler;

    public GetBookInventoriesPagedEndpoint(GetBookInventoriesPagedHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/bookinventories");

        Description(x =>
        {
            x.WithTags("BookInventory");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetBookInventoriesPagedQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}