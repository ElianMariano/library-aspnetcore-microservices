using BuildingBlocks.DataTransferObjects;
using Membership.Application.Dtos;

namespace Membership.Application.Handlers.Members.Queries.GetPaged;

public sealed class GetMembersPagedQuery(int currentPage = 1, int pageSize = 12) : PagedRequestBase(currentPage, pageSize);

public sealed class GetMembersPagedResult(IReadOnlyList<MemberDto> Data, int TotalItems, int currentPage = 1, int pageSize = 12) : PagedResponseBase<MemberDto>(Data, TotalItems, currentPage, pageSize);