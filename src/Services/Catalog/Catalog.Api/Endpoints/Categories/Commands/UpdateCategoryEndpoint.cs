using Catalog.Application.Handlers.Categories.Commands.Update;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Categories.Commands;

public class UpdateCategoryEndpoint : Endpoint<UpdateCategoryCommand, UpdateCategoryResult>
{
    private readonly UpdateCategoryHandler _handler;

    public UpdateCategoryEndpoint(UpdateCategoryHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Put("/category");

        Description(x =>
        {
            x.WithTags("Category");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}