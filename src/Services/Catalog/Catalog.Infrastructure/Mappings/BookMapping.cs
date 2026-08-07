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
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .HasConversion(id => id.Value, value => new BookId(value));
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(BookConstraints.BookTitleMaxCharacters);
        builder.Property(b => b.Isbn)
            .IsRequired()
            .HasMaxLength(BookConstraints.BookISBNMaxCharacters);
        builder.Property(b => b.PublicationYear)
            .IsRequired();
        builder.Property(b => b.AuthorId)
            .HasConversion(authorId => authorId.Value, value => new AuthorId(value));
        builder.HasOne(b => b.Author)
            .WithMany()
            .HasForeignKey(b => b.AuthorId);
        builder.Navigation(b => b.Author)
            .AutoInclude();
        builder.Property(b => b.CategoryId)
            .HasConversion(categoryId => categoryId.Value, value => new CategoryId(value));
        builder.HasOne(b => b.Category)
            .WithMany()
            .HasForeignKey(b => b.CategoryId);
        builder.Navigation(b => b.Category)
            .AutoInclude();
    }
}