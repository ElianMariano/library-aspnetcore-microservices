using FluentValidation;
using Loan.Application.Dtos;

namespace Loan.Application.Rules;

public static class LoanRegistryRules
{
    public static IRuleBuilderOptions<T, Guid> LoanRegistryId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }

    public static IRuleBuilderOptions<T, Guid> UserId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }

    public static IRuleBuilderOptions<T, DateOnly> LoanDate<T>(
        this IRuleBuilder<T, DateOnly> rule)
    {
        return rule.NotNull();
    }

    public static IRuleBuilderOptions<T, DateOnly> DueDate<T>(
        this IRuleBuilder<T, DateOnly> rule)
    {
        return rule.NotNull();
    }

    public static IRuleBuilderOptions<T, List<LoanItemDto>> Items<T>(
        this IRuleBuilder<T, List<LoanItemDto>> rule)
    {
        return rule.NotNull();
    }
}