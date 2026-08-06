using FastEndpoints;
using Membership.Application.Handlers.Members.Queries.GetPaged;

namespace Catalog.Api.Endpoints.Members.Queries;

public class GetMembersPagedEndpoint : Endpoint<GetMembersPagedQuery, GetMembersPagedResult>
{
    private readonly GetMembersPagedHandler _handler;

    public GetMembersPagedEndpoint(GetMembersPagedHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/members");

        Description(x =>
        {
            x.WithTags("Member");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetMembersPagedQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}