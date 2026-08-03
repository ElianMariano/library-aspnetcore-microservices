using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Entities;

public class Book
{
    public BookId Id { get; init; }

    public string Title { get; set; }

    public string ISBN { get; set; }

    public int PublicationYear { get; set; }

    public AuthorId AuthorId { get; set; }

    public CategoryId CategoryId { get; set; }

    public Book(
        string title,
        string isbn,
        int publicationYear,
        AuthorId authorId,
        CategoryId categoryId)
    {
        Id = new BookId(Guid.NewGuid());
        Title = title;
        ISBN = isbn;
        PublicationYear = publicationYear;
        AuthorId = authorId;
        CategoryId = categoryId;
    }

    public void Update(
        string title,
        string isbn,
        int publicationYear)
    {
        Title = title;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }
}