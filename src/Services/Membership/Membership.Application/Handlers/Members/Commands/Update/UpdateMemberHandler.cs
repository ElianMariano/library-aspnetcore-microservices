using BuildingBlocks;
using BuildingBlocks.Extensions;
using Membership.Application.Data;
using Membership.Application.Exceptions;
using Membership.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Membership.Application.Handlers.Members.Commands.Update;

public class UpdateMemberHandler(
    ILogger<UpdateMemberHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<UpdateMemberCommand, UpdateMemberResult>
{
    public async Task<UpdateMemberResult> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var memberId = new MemberId(request.member.memberId);
        var member = await context.Members.FindAsync([memberId], cancellationToken: cancellationToken);
        if (member is null)
        {
            throw new MemberNotFoundException(request.member.memberId);
        }
        member.Update(
            request.member.name,
            request.member.email);
        context.Members.Update(member);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogUpdateInformation(memberId.Value);
        return new UpdateMemberResult(memberId.Value);
    }
}