using BuildingBlocks.DataTransferObjects;
using FluentValidation;
using Loan.Application.Rules;

namespace Loan.Application.Handlers.LoanRegistries.Commands.Return;

public record ReturnLoanRegistryCommand(Guid loanRegistryId);

public class ReturnLoanRegistryResult(Guid Id) : ResponseBase<Guid?>(Id);

public class ReturnLoanRegistryValidator : AbstractValidator<ReturnLoanRegistryCommand>
{
    public ReturnLoanRegistryValidator()
    {
        RuleFor(x => x.loanRegistryId).LoanRegistryId();
    }
}