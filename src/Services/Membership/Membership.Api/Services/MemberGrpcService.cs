using Grpc.Core;
using Member.Grpc;
using Membership.Application.Data;
using Membership.Domain.ValueObjects;

namespace Membership.Api.Services;

public class MemberGrpcService(IApplicationDbContext dbContext) : MemberService.MemberServiceBase
{
    public override async Task<CanMakeLoanResponse> CanMakeLoan(CanMakeLoanRequest request, ServerCallContext context)
    {
        MemberId memberId = new MemberId(Guid.Parse(request.MemberId));
        var member = await dbContext.Members.FindAsync(memberId);
        bool ableToLoan = member!.AbleToLoan(request.Quantity);
        return new CanMakeLoanResponse
        {
            AbleToLoan = ableToLoan,
        };
    }
}