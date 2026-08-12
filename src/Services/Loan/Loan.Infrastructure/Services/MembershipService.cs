using Loan.Application.Services;
using Member.Grpc;

namespace Loan.Infrastructure.Services;

public class MembershipService : IMembershipService
{
    private readonly MemberService.MemberServiceClient _client;

    public MembershipService(MemberService.MemberServiceClient client)
    {
        _client = client;
    }

    public async Task<MembershipServiceResponse> CanMakeLoan(MembershipServiceRequest request)
    {
        var response = await _client.CanMakeLoanAsync(new CanMakeLoanRequest { MemberId = request.memberId.ToString(), Quantity = request.quantity});
        return new MembershipServiceResponse(response.AbleToLoan);
    }
}