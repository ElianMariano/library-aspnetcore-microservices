namespace Membership.Application.Services;

public interface IMemberService
{
    Task<MemberServiceResponse> CanMakeLoan(MemberServiceRequest request);
}