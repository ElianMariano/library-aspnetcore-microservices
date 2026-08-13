using Catalog.Application.Handlers.Categories.Queries.GetById;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Categories.Queries;

public class GetCategoryByIdEndpoint : Endpoint<GetCategoryByIdQuery, GetCategoryByIdResult>
{
    private readonly GetCategoryByIdHandler _handler;

    public GetCategoryByIdEndpoint(GetCategoryByIdHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/category/{{categoryId}}");

        Description(x =>
        {
            x.WithTags("Category");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}