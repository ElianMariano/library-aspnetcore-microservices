using FastEndpoints;
using Inventory.Application.Handlers.BookInventories.Commands.Delete;

namespace Inventory.Api.Endpoints.BookInventories.Commands;

public class DeleteBookInventoryEndpoint : Endpoint<DeleteBookInventoryCommand, DeleteBookInventoryResult>
{
    private readonly DeleteBookInventoryHandler _handler;

    public DeleteBookInventoryEndpoint(DeleteBookInventoryHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Delete($"/bookinventory/{{itemId}}");

        Description(x =>
        {
            x.WithTags("BookInventory");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteBookInventoryCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}