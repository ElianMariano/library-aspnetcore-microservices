using Catalog.Application.Handlers.Authors.Commands.Update;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Authors.Commands;

public class UpdateAuthorEndpoint : Endpoint<UpdateAuthorCommand, UpdateAuthorResult>
{
    private readonly UpdateAuthorHandler _handler;

    public UpdateAuthorEndpoint(UpdateAuthorHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Put("/author");

        Description(x =>
        {
            x.WithTags("Author");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        UpdateAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}