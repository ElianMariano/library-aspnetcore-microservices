using BuildingBlocks;
using BuildingBlocks.Extensions;
using Membership.Application.Data;
using Membership.Application.Exceptions;
using Membership.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Membership.Application.Handlers.Members.Commands.Delete;

public class DeleteMemberHandler(
    ILogger<DeleteMemberHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<DeleteMemberCommand, DeleteMemberResult>
{
    public async Task<DeleteMemberResult> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var memberId = new MemberId(request.memberId);
        var member = await context.Members.FindAsync([memberId], cancellationToken: cancellationToken);
        if (member is null)
        {
            throw new MemberNotFoundException(request.memberId);
        }
        context.Members.Remove(member);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogDeleteInformation(request.memberId);
        return new DeleteMemberResult(null);
    }
}