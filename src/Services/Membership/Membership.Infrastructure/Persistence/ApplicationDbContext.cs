using MassTransit;
using Membership.Application.Data;
using Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.Persistence;

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

    public DbSet<Member> Members => Set<Member>();
}