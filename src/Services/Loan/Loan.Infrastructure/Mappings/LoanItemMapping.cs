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
        builder.HasKey(lr => lr.Id);
        builder.Property(lr => lr.Id)
            .HasConversion(id => id.Value, value => new LoanItemId(value));
        builder.Property(lr => lr.LoanRegistryId)
            .HasConversion(id => id.Value, value => new LoanRegistryId(value))
            .IsRequired();
        builder.HasOne(lr => lr.LoanRegistry)
            .WithMany(lr => lr.Items)
            .HasForeignKey(li => li.LoanRegistryId)
            .HasPrincipalKey(lr => lr.Id);
        builder.Navigation(lr => lr.LoanRegistry)
            .AutoInclude();
        builder.Property(li => li.BookId)
            .IsRequired();
    }
}