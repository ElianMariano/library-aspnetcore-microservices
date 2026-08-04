using Catalog.Application.Handlers.Books.Commands.Update;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Books.Commands;

public class UpdateBookEndpoint : Endpoint<UpdateBookCommand, UpdateBookResult>
{
    private readonly UpdateBookHandler _handler;

    public UpdateBookEndpoint(UpdateBookHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Put("/book");

        Description(x =>
        {
            x.WithTags("Book");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        UpdateBookCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}