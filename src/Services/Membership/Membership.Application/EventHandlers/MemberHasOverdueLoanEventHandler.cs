using BuildingBlocks.Messaging.Events;
using MassTransit;
using Membership.Application.Data;
using Membership.Application.Exceptions;
using Membership.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Membership.Application.EventHandlers;

public class MemberHasOverdueLoanEventHandler(
    IApplicationDbContext dbContext,
    ILogger<MemberHasOverdueLoanEventHandler> logger) : IConsumer<MemberHasOverdueLoanEvent>
{
    public async Task Consume(ConsumeContext<MemberHasOverdueLoanEvent> context)
    {
        logger.LogInformation("Membership consumed event for member {0}", context.Message.userId);
        var memberId = new MemberId(context.Message.userId);
        var member = await dbContext.Members.FindAsync([memberId]);
        if (member == null)
        {
            throw new MemberNotFoundException(memberId.Value);
        }
        member.SetOverdueLoan(false);
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }
}