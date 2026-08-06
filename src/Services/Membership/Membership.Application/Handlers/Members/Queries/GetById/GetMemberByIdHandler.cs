using BuildingBlocks;
using Loan.Application.Handlers.Members.Queries.GetById;
using Membership.Application.Data;
using Membership.Application.Dtos;
using Membership.Application.Exceptions;
using Membership.Domain.ValueObjects;

namespace Membership.Application.Handlers.Members.Queries.GetById;

public class GetMemberByIdHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetMemberByIdQuery, GetMemberByIdResult>
{
    public async Task<GetMemberByIdResult> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var memberId = new MemberId(request.memberId);
        var member = await context.Members.FindAsync([memberId], cancellationToken: cancellationToken);
        if (member is null)
        {
            throw new MemberNotFoundException(request.memberId);
        }
        var data = new MemberDto(
            member.Id.Value,
            member.Name,
            member.Email,
            member.Status,
            member.ActiveLoans,
            member.MaxLoans,
            member.HasOverdueLoan);
        return new GetMemberByIdResult(data);
    }
}