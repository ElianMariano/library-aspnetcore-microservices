using Catalog.Domain.Constraints;
using FluentValidation;

namespace Catalog.Application.Rules;

public static class CategoryRules
{
    public static IRuleBuilderOptions<T, Guid> CategoryId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }

    public static IRuleBuilderOptions<T, string> CategoryName<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty()
            .MaximumLength(CategoryConstraints.CategoryNameMaxCharacters);
    }
}