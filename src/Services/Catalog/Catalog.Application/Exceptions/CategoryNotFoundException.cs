namespace Catalog.Application.Exceptions;

public sealed class CategoryNotFoundException : ApplicationException
{
    public CategoryNotFoundException(Guid categoryId) : base("Category not found", categoryId)
    {
    }
}