using Membership.Domain.Enumerations;

namespace Membership.Application.Dtos;

public record MemberDto(
    Guid memberId,
    string name,
    string email,
    MemberStatus status,
    int activeLoans,
    int maxLoans,
    bool hasOverdueLoan);