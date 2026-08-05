using BuildingBlocks;
using Loan.Application.Data;
using Loan.Application.Exceptions;
using Loan.Domain.ValueObjects;

namespace Loan.Application.Handlers.LoanRegistries.Commands.Return;

public class ReturnLoanRegistryHandler(
    IApplicationDbContext context)
    : IApplicationHandler<ReturnLoanRegistryCommand, ReturnLoanRegistryResult>
{
    public async Task<ReturnLoanRegistryResult> Handle(ReturnLoanRegistryCommand request, CancellationToken cancellationToken)
    {
        var loanRegistryId = new LoanRegistryId(request.loanRegistryId);
        var loanRegistry = await context.LoanRegistries.FindAsync([loanRegistryId], cancellationToken);
        if (loanRegistry == null)
        {
            throw new LoanRegistryNotFoundException(loanRegistryId.Value);
        }
        loanRegistry.ReturnLoan();
        context.LoanRegistries.Update(loanRegistry);
        await context.SaveChangesAsync(cancellationToken);
        return new ReturnLoanRegistryResult(loanRegistry.Id!.Value);
    }
}