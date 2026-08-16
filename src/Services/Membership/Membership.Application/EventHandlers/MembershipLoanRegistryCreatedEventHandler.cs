using BuildingBlocks.Messaging.Events;
using MassTransit;
using Membership.Application.Data;
using Membership.Application.Exceptions;
using Membership.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Membership.Application.EventHandlers;

public class MembershipLoanRegistryCreatedEventHandler(IApplicationDbContext dbContext, ILogger<MembershipLoanRegistryCreatedEventHandler> logger) : IConsumer<LoanRegistryCreatedEvent>
{
    public async Task Consume(ConsumeContext<LoanRegistryCreatedEvent> context)
    {
        logger.LogInformation("Membership consumed event for loan registry {0}", context.Message.loanRegistryId);
        MemberId memberId = new MemberId(context.Message.userId);
        var member = await dbContext.Members.FindAsync(memberId);
        if (member == null)
        {
            throw new MemberNotFoundException(context.Message.userId);
        }
        // TODO: Verificar essa propriedade count
        member.AddNewActiveLoans(context.Message.items.Count);
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }
}