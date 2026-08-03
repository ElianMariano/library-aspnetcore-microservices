using Catalog.Domain.Constraints;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Mappings;

public class BookMapping : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable(nameof(Book));
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .HasConversion(id => id.Value, value => new BookId(value));
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(BookConstraints.BookTitleMaxCharacters);
        builder.Property(c => c.ISBN)
            .IsRequired()
            .HasMaxLength(BookConstraints.BookISBNMaxCharacters);
    }
}