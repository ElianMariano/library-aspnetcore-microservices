namespace Membership.Application.Services;

public record MemberServiceRequest(Guid memberId, int quantity);