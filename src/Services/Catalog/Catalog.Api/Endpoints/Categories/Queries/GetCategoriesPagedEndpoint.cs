using Catalog.Application.Handlers.Categories.Queries.GetPaged;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Categories.Queries;

public class GetCategoriesPagedEndpoint : Endpoint<GetCategoriesPagedQuery, GetCategoriesPagedResult>
{
    private readonly GetCategoriesPagedHandler _handler;

    public GetCategoriesPagedEndpoint(GetCategoriesPagedHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/categories");

        Description(x =>
        {
            x.WithTags("Category");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetCategoriesPagedQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}