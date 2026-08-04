using FluentValidation;

namespace Inventory.Application.Rules;

public static class ReservationRules
{
    public static IRuleBuilderOptions<T, Guid> ReservationId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }

    public static IRuleBuilderOptions<T, Guid> BookId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }

    public static IRuleBuilderOptions<T, Guid> AuthorId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }
}
