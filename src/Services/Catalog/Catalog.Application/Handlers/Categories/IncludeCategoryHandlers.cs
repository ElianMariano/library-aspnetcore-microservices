using Catalog.Application.Handlers.Categories.Commands.Create;
using Catalog.Application.Handlers.Categories.Commands.Delete;
using Catalog.Application.Handlers.Categories.Commands.Update;
using Catalog.Application.Handlers.Categories.Queries.GetById;
using Catalog.Application.Handlers.Categories.Queries.GetPaged;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Handlers.Categorys;

public static class IncludeCategoryHandlers
{
    public static void Include(IServiceCollection builder)
    {
        builder.AddScoped<CreateCategoryHandler>();
        builder.AddScoped<UpdateCategoryHandler>();
        builder.AddScoped<DeleteCategoryHandler>();
        builder.AddScoped<GetCategoryByIdHandler>();
        builder.AddScoped<GetCategoriesPagedHandler>();
    }
}