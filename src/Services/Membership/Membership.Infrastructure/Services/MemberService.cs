using Membership.Application.Data;
using Membership.Application.Services;

namespace Membership.Infrastructure.Services;

public class MemberService(IApplicationDbContext dbContext) : IMemberService
{
    public async Task<MemberServiceResponse> CanMakeLoan(MemberServiceRequest request)
    {
        var member = await dbContext.Members.FindAsync(request.memberId);
        bool ableToLoan = member!.AbleToLoan(request.quantity);
        return new MemberServiceResponse(ableToLoan);
    }
}