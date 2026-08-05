using BuildingBlocks.DataTransferObjects;
using FluentValidation;
using Loan.Application.Dtos;
using Loan.Application.Rules;

namespace Loan.Application.Handlers.LoanRegistries.Commands.Create;

public record CreateLoanRegistryCommand(LoanRegistryDto loanRegistry);

public class CreateLoanRegistryResult(Guid Id) : ResponseBase<Guid?>(Id);

public class CreateLoanRegistryValidator : AbstractValidator<CreateLoanRegistryCommand>
{
    public CreateLoanRegistryValidator()
    {
        RuleFor(x => x.loanRegistry.userId).UserId();

        RuleFor(x => x.loanRegistry.loanDate).LoanDate();

        RuleFor(x => x.loanRegistry.dueDate).DueDate();

        RuleFor(x => x.loanRegistry.items).Items();
    }
}