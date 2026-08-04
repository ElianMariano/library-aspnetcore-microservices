using FluentValidation;

namespace Inventory.Application.Rules;

public static class BookInventoryRules
{
    public static IRuleBuilderOptions<T, Guid> BookInventoryId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }
}
