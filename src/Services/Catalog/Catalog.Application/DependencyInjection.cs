using Catalog.Application.Handlers.Books;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection builder)
    {
        IncludeBooksHandler.Include(builder);
    }
}