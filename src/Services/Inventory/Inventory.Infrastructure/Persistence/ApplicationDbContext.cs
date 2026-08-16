using Inventory.Application.Data;
using Inventory.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.AddTransactionalOutboxEntities();
    }

    public DbSet<BookInventory> BookInventories => Set<BookInventory>();

    public DbSet<Reservation> Reservations => Set<Reservation>();
}