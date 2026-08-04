using Catalog.Application.Handlers.Authors.Commands.Create;
using Catalog.Application.Handlers.Authors.Commands.Delete;
using Catalog.Application.Handlers.Authors.Commands.Update;
using Catalog.Application.Handlers.Authors.Queries.GetById;
using Catalog.Application.Handlers.Authors.Queries.GetPaged;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Handlers.Authors;

public static class IncludeAuthorHandlers
{
    public static void Include(IServiceCollection builder)
    {
        builder.AddScoped<CreateAuthorHandler>();
        builder.AddScoped<UpdateAuthorHandler>();
        builder.AddScoped<DeleteAuthorHandler>();
        builder.AddScoped<GetAuthorByIdHandler>();
        builder.AddScoped<GetAuthorsPagedHandler>();
    }
}