namespace Catalog.Application.Dtos;

public record BookDto(Guid id, string title, string isbn, int publicationYear, Guid authorId, Guid categoryId);