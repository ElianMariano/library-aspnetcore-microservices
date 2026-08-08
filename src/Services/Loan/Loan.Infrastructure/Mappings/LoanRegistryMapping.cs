using Loan.Domain.Entities;
using Loan.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.Infrastructure.Mappings;

public class LoanRegistryMapping : IEntityTypeConfiguration<LoanRegistry>
{
    public void Configure(EntityTypeBuilder<LoanRegistry> builder)
    {
        builder.ToTable(nameof(LoanRegistry));
        builder.HasKey(lr => lr.Id);
        builder.Property(lr => lr.Id)
            .HasConversion(id => id.Value, value => new LoanRegistryId(value))
            .IsRequired();
        builder.Property(lr => lr.UserId)
            .IsRequired();
        builder.Property(lr => lr.LoanDate)
            .IsRequired();
        builder.Property(lr => lr.DueDate)
            .IsRequired();
        builder.Property(lr => lr.ReturnedDate)
            .IsRequired(false);
        builder.Property(lr => lr.Status)
            .IsRequired();
        builder.HasMany(lr => lr.Items)
            .WithOne(li => li.LoanRegistry)
            .HasForeignKey(li => li.LoanRegistryId)
            .IsRequired();
    }
}