using Catalog.Application.Handlers.Books.Commands.Create;
using Catalog.Application.Handlers.Books.Commands.Delete;
using Catalog.Application.Handlers.Books.Commands.Update;
using Catalog.Application.Handlers.Books.Queries.GetById;
using Catalog.Application.Handlers.Books.Queries.GetPaged;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Handlers.Books;

public static class IncludeBooksHandler
{
    public static void Include(IServiceCollection builder)
    {
        builder.AddScoped<CreateBookHandler>();
        builder.AddScoped<UpdateBookHandler>();
        builder.AddScoped<DeleteBookHandler>();
        builder.AddScoped<GetBookByIdHandler>();
        builder.AddScoped<GetBooksPagedHandler>();
    }
}