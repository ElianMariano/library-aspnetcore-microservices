using BuildingBlocks.Messaging.Events;
using MassTransit;
using Membership.Application.Data;
using Membership.Application.Exceptions;
using Membership.Domain.ValueObjects;

namespace Membership.Application.EventHandlers;

public class LoanRegistryReturnedEventHandler(IApplicationDbContext dbContext) : IConsumer<LoanRegistryReturnedEvent>
{
    public async Task Consume(ConsumeContext<LoanRegistryReturnedEvent> context)
    {
        MemberId memberId = new MemberId(context.Message.userId);
        var member = await dbContext.Members.FindAsync(memberId);
        if (member == null)
        {
            throw new MemberNotFoundException(context.Message.userId);
        }
        member.RemoveActiveLoans(context.Message.items.Count);
    }
}