using Membership.Application.Data;
using Membership.Application.Services;
using Membership.Domain.ValueObjects;

namespace Membership.Infrastructure.Services;

public class MemberService(IApplicationDbContext dbContext) : IMemberService
{
    public async Task<MemberServiceResponse> CanMakeLoan(MemberServiceRequest request)
    {
        var memberId = new MemberId(request.memberId);
        var member = await dbContext.Members.FindAsync([memberId]);
        if (member == null)
        {
            return new MemberServiceResponse(false);
        }
        bool ableToLoan = member!.AbleToLoan(request.quantity);
        return new MemberServiceResponse(ableToLoan);
    }
}