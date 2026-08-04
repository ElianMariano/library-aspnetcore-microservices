using Catalog.Application.Handlers.Authors.Commands.Create;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Authors.Commands;

public class CreateAuthorEndpoint : Endpoint<CreateAuthorCommand, CreateAuthorResult>
{
    private readonly CreateAuthorHandler _handler;

    public CreateAuthorEndpoint(CreateAuthorHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Post("/author");

        Description(x =>
        {
            x.WithTags("Author");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}