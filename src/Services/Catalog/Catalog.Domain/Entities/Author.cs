using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Entities;

public class Author
{
    public AuthorId Id { get; init; }

    public string Name { get; set; }

    public Author(string name)
    {
        Id = new AuthorId(Guid.NewGuid());
        Name = name;
    }

    public void Update(string name)
    {
        Name = name;
    }
}