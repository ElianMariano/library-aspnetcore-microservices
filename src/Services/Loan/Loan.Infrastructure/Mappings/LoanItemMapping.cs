using Loan.Domain.Entities;
using Loan.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.Infrastructure.Mappings;

public class LoanItemMapping : IEntityTypeConfiguration<LoanItem>
{
    public void Configure(EntityTypeBuilder<LoanItem> builder)
    {
        builder.ToTable(nameof(LoanItem));
        builder.HasKey(lr => new { lr.LoanRegistryId, lr.BookId});
        builder.Property(lr => lr.LoanRegistryId)
            .HasConversion(id => id.Value, value => new LoanRegistryId(value))
            .IsRequired();
        builder.Property(lr => lr.BookId)
            .IsRequired();
        builder.HasOne(x => x.LoanRegistry)
            .WithMany(x => x._items)
            .HasForeignKey(x => x.LoanRegistryId);
    }
}