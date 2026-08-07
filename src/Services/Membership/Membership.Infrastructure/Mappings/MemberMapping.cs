using Membership.Domain.Constraints;
using Membership.Domain.Entities;
using Membership.Domain.Enumerations;
using Membership.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.Mappings;

public class MemberMapping : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable(nameof(Member));
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .HasConversion(id => id.Value, value => new MemberId(value))
            .IsRequired();
        builder.Property(m => m.Name)
            .HasMaxLength(MemberConstraints.MaxNameLength)
            .IsRequired();
        builder.Property(m => m.Email)
            .HasMaxLength(MemberConstraints.MaxEmailLength)
            .IsRequired();
        builder.Property(c => c.Status)
            .HasConversion(
                status => status.ToString(),
                status => Enum.Parse<MemberStatus>(status))
            .HasMaxLength(MemberConstraints.MaxStatusLength)
            .IsRequired();
        builder.Property(m => m.ActiveLoans)
            .IsRequired();
        builder.Property(m => m.MaxLoans)
            .IsRequired();
        builder.Property(m => m.HasOverdueLoan)
            .IsRequired();
    }
}
