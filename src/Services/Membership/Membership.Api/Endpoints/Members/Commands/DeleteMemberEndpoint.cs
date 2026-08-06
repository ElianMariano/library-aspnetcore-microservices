using FastEndpoints;
using Membership.Application.Handlers.Members.Commands.Delete;

namespace Catalog.Api.Endpoints.Members.Commands;

public class DeleteMemberEndpoint : Endpoint<DeleteMemberCommand, DeleteMemberResult>
{
    private readonly DeleteMemberHandler _handler;

    public DeleteMemberEndpoint(DeleteMemberHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Delete($"/member/{{memberId}}");

        Description(x =>
        {
            x.WithTags("Member");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteMemberCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}