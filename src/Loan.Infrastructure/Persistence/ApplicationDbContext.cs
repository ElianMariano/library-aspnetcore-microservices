using Loan.Application.Data;
using Loan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loan.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<LoanRegistry> LoanRegistries => Set<LoanRegistry>();

    public DbSet<LoanItem> LoanItems => Set<LoanItem>();
}