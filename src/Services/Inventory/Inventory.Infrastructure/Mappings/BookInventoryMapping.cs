using Inventory.Domain.Entities;
using Inventory.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Mappings;

public class BookInventoryMapping : IEntityTypeConfiguration<BookInventory>
{
    public void Configure(EntityTypeBuilder<BookInventory> builder)
    {
        builder.ToTable(nameof(BookInventory));
        builder.HasKey(bi => bi.Id);
        builder.Property(bi => bi.Id)
            .HasConversion(id => id.Value, value => new BookInventoryId(value));
        builder.Property(bi => bi.BookId)
            .IsRequired();
        builder.Ignore(bi => bi.TotalCopies);
        builder.Property(bi => bi.AvailableCopies)
            .IsRequired();
        builder.Property(bi => bi.ReservedCopies)
            .IsRequired();
    }
}