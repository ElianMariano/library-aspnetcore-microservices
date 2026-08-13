using Catalog.Application.Handlers.Authors.Queries.GetById;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Authors.Queries;

public class GetAuthorByIdEndpoint : Endpoint<GetAuthorByIdQuery, GetAuthorByIdResult>
{
    private readonly GetAuthorByIdHandler _handler;

    public GetAuthorByIdEndpoint(GetAuthorByIdHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/author/{{authorId}}");

        Description(x =>
        {
            x.WithTags("Author");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetAuthorByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}