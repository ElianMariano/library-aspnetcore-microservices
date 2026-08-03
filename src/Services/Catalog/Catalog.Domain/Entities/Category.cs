using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Entities;

public class Category
{
    public CategoryId Id { get; init; }

    public string Name { get; set; }

    public Category(string name)
    {
        Id = new CategoryId(Guid.NewGuid());
        Name = name;
    }

    public void Update(string name)
    {
        Name = name;
    }
}