using Microsoft.EntityFrameworkCore;
using Loan.Domain.Entities;

namespace Loan.Application.Data;

public interface IApplicationDbContext
{
    DbSet<LoanRegistry> LoanRegistries { get; }

    DbSet<LoanItem> LoanItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}