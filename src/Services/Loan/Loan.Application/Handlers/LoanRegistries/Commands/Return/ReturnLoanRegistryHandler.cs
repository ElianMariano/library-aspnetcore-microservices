using BuildingBlocks;
using BuildingBlocks.Messaging.Events;
using Loan.Application.Data;
using Loan.Application.Exceptions;
using Loan.Domain.Entities;
using Loan.Domain.ValueObjects;
using MassTransit;

namespace Loan.Application.Handlers.LoanRegistries.Commands.Return;

public class ReturnLoanRegistryHandler(
    IApplicationDbContext context,
    IPublishEndpoint publishEndpoint)
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
        await publishEvent(loanRegistry, cancellationToken);
        return new ReturnLoanRegistryResult(loanRegistry.Id!.Value);
    }

    private async Task publishEvent(LoanRegistry loanRegistry, CancellationToken cancellationToken)
    {
        var loanRegistryCreatedEvent = new LoanRegistryReturnedEvent(
            loanRegistry.Id!.Value,
            loanRegistry.UserId,
            loanRegistry.LoanDate,
            loanRegistry.DueDate,
            loanRegistry.ReturnedDate,
            loanRegistry.Status.ToString(),
            loanRegistry.Items.Select(i => i.BookId).ToList());
        await publishEndpoint.Publish(loanRegistryCreatedEvent, cancellationToken);
    }
}