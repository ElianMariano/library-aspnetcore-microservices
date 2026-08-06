using Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Member> Members { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}