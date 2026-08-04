using Catalog.Domain.Constraints;
using FluentValidation;

namespace Catalog.Application.Rules;

public static class AuthorRules
{
    public static IRuleBuilderOptions<T, Guid> AuthorId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }

    public static IRuleBuilderOptions<T, string> AuthorName<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty()
            .MaximumLength(AuthorConstraints.AuthorNameMaxCharacters);
    }
}