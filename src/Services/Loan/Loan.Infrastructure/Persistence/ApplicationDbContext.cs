using Loan.Application.Data;
using Loan.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Loan.Infrastructure.Persistence;

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

    public DbSet<LoanRegistry> LoanRegistries => Set<LoanRegistry>();

    public DbSet<LoanItem> LoanItems => Set<LoanItem>();
}