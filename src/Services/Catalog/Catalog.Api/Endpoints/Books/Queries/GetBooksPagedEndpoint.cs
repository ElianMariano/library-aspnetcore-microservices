using Catalog.Application.Handlers.Books.Queries.GetById;
using Catalog.Application.Handlers.Books.Queries.GetPaged;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Books.Queries;

public class GetBooksPagedEndpoint : Endpoint<GetBooksPagedQuery, GetBooksPagedResult>
{
    private readonly GetBooksPagedHandler _handler;

    public GetBooksPagedEndpoint(GetBooksPagedHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/books");

        Description(x =>
        {
            x.WithTags("Book");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetBooksPagedQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}