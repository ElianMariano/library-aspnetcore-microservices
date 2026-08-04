using Catalog.Application.Handlers.Books.Commands.Create;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Books.Commands;

public class CreateBookEndpoint : Endpoint<CreateBookCommand, CreateBookResult>
{
    private readonly CreateBookHandler _handler;

    public CreateBookEndpoint(CreateBookHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Post("/book");

        Description(x =>
        {
            x.WithTags("Book");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateBookCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}