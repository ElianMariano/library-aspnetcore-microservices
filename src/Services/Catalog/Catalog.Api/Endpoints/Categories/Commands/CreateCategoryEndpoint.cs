using Catalog.Application.Handlers.Categories.Commands.Create;
using FastEndpoints;

namespace Catalog.Api.Endpoints.Categories.Commands;

public class CreateCategoryEndpoint : Endpoint<CreateCategoryCommand, CreateCategoryResult>
{
    private readonly CreateCategoryHandler _handler;

    public CreateCategoryEndpoint(CreateCategoryHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Post("/category");

        Description(x =>
        {
            x.WithTags("Category");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}