namespace Loan.Application.Services;

public interface IMembershipService
{
    Task<MembershipServiceResponse> CanMakeLoan(MembershipServiceRequest request);
}