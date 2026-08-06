using FluentValidation;
using Membership.Domain.Constraints;

namespace Membership.Application.Rules;

public static class MemberRules
{
    public static IRuleBuilderOptions<T, Guid> MemberId<T>(
        this IRuleBuilder<T, Guid> rule)
    {
        return rule.NotEmpty();
    }

    public static IRuleBuilderOptions<T, string> MemberName<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty()
            .MaximumLength(MemberConstraints.MaxNameLength);
    }

    public static IRuleBuilderOptions<T, string> MemberEmail<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty()
            .MaximumLength(MemberConstraints.MaxEmailLength);
    }
}