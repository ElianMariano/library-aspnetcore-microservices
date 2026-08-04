using Catalog.Application.Handlers.Authors.Commands.Delete;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Authors.Commands;

public class DeleteAuthorEndpoint : Endpoint<DeleteAuthorCommand, DeleteAuthorResult>
{
    private readonly DeleteAuthorHandler _handler;

    public DeleteAuthorEndpoint(DeleteAuthorHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Delete($"/author/{{itemId}}");

        Description(x =>
        {
            x.WithTags("Author");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}