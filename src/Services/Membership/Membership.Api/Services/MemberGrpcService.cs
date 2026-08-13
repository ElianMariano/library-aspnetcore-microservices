using Grpc.Core;
using Member.Grpc;
using Membership.Application.Services;

namespace Membership.Api.Services;

public class MemberGrpcService(IMemberService service) : MemberService.MemberServiceBase
{
    public override async Task<CanMakeLoanResponse> CanMakeLoan(CanMakeLoanRequest request, ServerCallContext context)
    {
        var response = await service.CanMakeLoan(new MemberServiceRequest(Guid.Parse(request.MemberId), request.Quantity));
        return new CanMakeLoanResponse
        {
            AbleToLoan = response.ableToLoan,
        };
    }
}