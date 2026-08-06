using FastEndpoints;
using Membership.Application.Handlers.Members.Commands.Update;

namespace Catalog.Api.Endpoints.Members.Commands;

public class UpdateMemberEndpoint : Endpoint<UpdateMemberCommand, UpdateMemberResult>
{
    private readonly UpdateMemberHandler _handler;

    public UpdateMemberEndpoint(UpdateMemberHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Put("/member");

        Description(x =>
        {
            x.WithTags("Member");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        UpdateMemberCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}