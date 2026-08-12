using BuildingBlocks.Messaging.Events;
using MassTransit;
using Membership.Application.Data;
using Membership.Application.Exceptions;
using Membership.Domain.ValueObjects;

namespace Membership.Application.EventHandlers;

public class LoanRegistryCreatedEventHandler(IApplicationDbContext dbContext) : IConsumer<LoanRegistryCreatedEvent>
{
    public async Task Consume(ConsumeContext<LoanRegistryCreatedEvent> context)
    {
        MemberId memberId = new MemberId(context.Message.userId);
        var member = await dbContext.Members.FindAsync(memberId);
        if (member == null)
        {
            throw new MemberNotFoundException(context.Message.userId);
        }
        member.AddNewActiveLoans(context.Message.items.Count);
    }
}