using Catalog.Application.Handlers.Authors;
using Catalog.Application.Handlers.Books;
using Catalog.Application.Handlers.Categorys;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection builder)
    {
        IncludeBookHandlers.Include(builder);
        IncludeAuthorHandlers.Include(builder);
        IncludeCategoryHandlers.Include(builder);
    }
}