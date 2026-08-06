using FastEndpoints;
using Loan.Application.Handlers.Members.Queries.GetById;
using Membership.Application.Handlers.Members.Queries.GetById;

namespace Catalog.Api.Endpoints.Members.Queries;

public class GetMemberByIdEndpoint : Endpoint<GetMemberByIdQuery, GetMemberByIdResult>
{
    private readonly GetMemberByIdHandler _handler;

    public GetMemberByIdEndpoint(GetMemberByIdHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/member");

        Description(x =>
        {
            x.WithTags("Member");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}