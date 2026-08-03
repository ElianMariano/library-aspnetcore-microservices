using Catalog.Domain.Constraints;
using FluentValidation;

namespace Catalog.Application.Rules;

public static class BookRules
{
    public static IRuleBuilderOptions<T, Guid> BookId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }

    public static IRuleBuilderOptions<T, string> BookTitle<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty()
            .MaximumLength(BookConstraints.BookTitleMaxCharacters);
    }

    public static IRuleBuilderOptions<T, string> BookISBN<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty()
            .MaximumLength(BookConstraints.BookISBNMaxCharacters);
    }

    public static IRuleBuilderOptions<T, Guid> AuthorId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }
    public static IRuleBuilderOptions<T, Guid> CategoryId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }

}