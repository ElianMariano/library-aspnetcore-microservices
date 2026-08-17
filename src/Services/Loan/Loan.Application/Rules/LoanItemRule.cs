using FluentValidation;
using Loan.Application.Dtos;

namespace Loan.Application.Rules;

public class LoanItemRule : AbstractValidator<LoanItemDto>
{
    public LoanItemRule()
    {
        RuleFor(x => x.bookId).NotEmpty();

        RuleFor(x => x.quantity).NotNull().GreaterThan(0);
    }
}