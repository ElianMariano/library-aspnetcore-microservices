using FastEndpoints;
using Membership.Application.Handlers.Members.Commands.Create;

namespace Catalog.Api.Endpoints.Members.Commands;

public class CreateMemberEndpoint : Endpoint<CreateMemberCommand, CreateMemberResult>
{
    private readonly CreateMemberHandler _handler;

    public CreateMemberEndpoint(CreateMemberHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Post("/member");

        Description(x =>
        {
            x.WithTags("Member");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateMemberCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}