using BuildingBlocks.DataTransferObjects;
using Membership.Application.Dtos;

namespace Loan.Application.Handlers.Members.Queries.GetById;

public record GetMemberByIdQuery(Guid memberId);

public sealed class GetMemberByIdResult(MemberDto Data, int StatusCode = 200) : ResponseBase<MemberDto>(Data, StatusCode);