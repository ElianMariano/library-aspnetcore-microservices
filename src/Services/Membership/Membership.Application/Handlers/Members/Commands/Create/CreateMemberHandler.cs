using BuildingBlocks;
using BuildingBlocks.Extensions;
using Membership.Application.Data;
using Membership.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Membership.Application.Handlers.Members.Commands.Create;

public class CreateMemberHandler(
    ILogger<CreateMemberHandler> logger,
    IApplicationDbContext context)
    : IApplicationHandler<CreateMemberCommand, CreateMemberResult>
{
    public async Task<CreateMemberResult> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = new Member(
            request.member.name,
            request.member.email,
            request.member.status,
            request.member.maxLoans);
        await context.Members.AddAsync(member, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogCreateInformation(member.Id!.Value);
        return new CreateMemberResult(member.Id!.Value);
    }
}