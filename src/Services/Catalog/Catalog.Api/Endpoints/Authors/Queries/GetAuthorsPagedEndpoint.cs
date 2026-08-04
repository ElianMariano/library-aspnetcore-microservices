using Catalog.Application.Handlers.Authors.Queries.GetPaged;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Authors.Queries;

public class GetAuthorsPagedEndpoint : Endpoint<GetAuthorsPagedQuery, GetAuthorsPagedResult>
{
    private readonly GetAuthorsPagedHandler _handler;

    public GetAuthorsPagedEndpoint(GetAuthorsPagedHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/authors");

        Description(x =>
        {
            x.WithTags("Author");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetAuthorsPagedQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}