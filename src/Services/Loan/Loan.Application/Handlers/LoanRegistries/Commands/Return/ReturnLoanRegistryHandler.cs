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
        var userId = loanRegistry.UserId;
        loanRegistry.ReturnLoan();
        context.LoanRegistries.Update(loanRegistry);
        await context.SaveChangesAsync(cancellationToken);
        await publishEvent(loanRegistry, cancellationToken);
        await UpdateMemberEligibility(userId, cancellationToken);
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
            loanRegistry.Items.Select(i => new LoanItemEventDto(i.BookId, i.Quantity)).ToList());
        await publishEndpoint.Publish(loanRegistryCreatedEvent, cancellationToken);
    }

    private async Task UpdateMemberEligibility(Guid userId, CancellationToken cancellationToken)
    {
        var loans = context.LoanRegistries.FirstOrDefault(x => x.UserId == userId);
        if (loans != null)
        {
            return;
        }
        var memberLoanEligibilityRestoredEvent = new MemberLoanEligibilityRestoredEvent(userId);
        await publishEndpoint.Publish(memberLoanEligibilityRestoredEvent, cancellationToken);
    }
}