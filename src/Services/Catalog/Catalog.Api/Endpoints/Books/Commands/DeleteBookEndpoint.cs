using Catalog.Application.Handlers.Books.Commands.Delete;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Books.Commands;

public class DeleteBookEndpoint : Endpoint<DeleteBookCommand, DeleteBookResult>
{
    private readonly DeleteBookHandler _handler;

    public DeleteBookEndpoint(DeleteBookHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Delete($"/book/{{itemId}}");

        Description(x =>
        {
            x.WithTags("Book");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteBookCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}