using FastEndpoints;
using Inventory.Application.Handlers.BookInventories.Commands.Create;

namespace Inventory.Api.Endpoints.BookInventories.Commands;

public class CreateBookInventoryEndpoint : Endpoint<CreateBookInventoryCommand, CreateBookInventoryResult>
{
    private readonly CreateBookInventoryHandler _handler;

    public CreateBookInventoryEndpoint(CreateBookInventoryHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Post("/bookinventory");

        Description(x =>
        {
            x.WithTags("BookInventory");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateBookInventoryCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}