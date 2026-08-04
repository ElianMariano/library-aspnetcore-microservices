using Catalog.Application.Handlers.Categories.Commands.Delete;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Categories.Commands;

public class DeleteCategoryEndpoint : Endpoint<DeleteCategoryCommand, DeleteCategoryResult>
{
    private readonly DeleteCategoryHandler _handler;

    public DeleteCategoryEndpoint(DeleteCategoryHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Delete($"/category/{{itemId}}");

        Description(x =>
        {
            x.WithTags("Category");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}