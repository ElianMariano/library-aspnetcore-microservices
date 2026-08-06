using BuildingBlocks;
using Membership.Application.Data;
using Membership.Application.Dtos;
using Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Application.Handlers.Members.Queries.GetPaged;

public class GetMembersPagedHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetMembersPagedQuery, GetMembersPagedResult>
{
    public async Task<GetMembersPagedResult> Handle(GetMembersPagedQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Member> query = context.Members.AsNoTracking();
        int totalItems = await query.CountAsync(cancellationToken);
        var members = await query
            .Skip((request.CurrentPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var data = members.Select(member => new MemberDto(
            member.Id.Value,
            member.Name,
            member.Email,
            member.Status,
            member.ActiveLoans,
            member.MaxLoans,
            member.HasOverdueLoan)).ToList();
        return new GetMembersPagedResult(data, totalItems, request.CurrentPage, request.PageSize);
    }
}