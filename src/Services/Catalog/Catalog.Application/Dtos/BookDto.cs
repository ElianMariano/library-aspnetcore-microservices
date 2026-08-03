namespace Catalog.Application.Dtos;

public record BookDto(Guid bookId, string title, string isbn, int publicationYear, Guid authorId, Guid categoryId);