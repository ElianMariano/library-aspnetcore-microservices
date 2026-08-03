using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }

    DbSet<Author> Authors { get; }

    DbSet<Category> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}