using Catalog.Application.Handlers.Books.Queries.GetById;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Books.Queries;

public class GetBookByIdEndpoint : Endpoint<GetBookByIdQuery, GetBookByIdResult>
{
    private readonly GetBookByIdHandler _handler;

    public GetBookByIdEndpoint(GetBookByIdHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/book/{{bookId}}");

        Description(x =>
        {
            x.WithTags("Book");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetBookByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}