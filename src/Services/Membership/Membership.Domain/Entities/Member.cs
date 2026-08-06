using Membership.Domain.Enumerations;
using Membership.Domain.ValueObjects;

namespace Membership.Domain.Entities;

public class Member
{
    public MemberId Id { get; private init; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public MemberStatus Status { get; private set; }

    public int ActiveLoans { get; private set; }

    public int MaxLoans { get; private set; }

    public bool HasOverdueLoan { get; private set; }

    public Member(
        string name,
        string email,
        MemberStatus status,
        int activeLoans,
        int maxLoans,
        bool hasOverdueLoan)
    {
        Id = new MemberId(Guid.NewGuid());
        Name = name;
        Email = email;
        Status = status;
        ActiveLoans = activeLoans;
        MaxLoans = maxLoans;
        HasOverdueLoan = hasOverdueLoan;
    }

    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public void UpdateLoanInfo(
        int activeLoans,
        int maxLoans,
        bool hasOverdueLoan)
    {
        ActiveLoans = activeLoans;
        MaxLoans = maxLoans;
        HasOverdueLoan = hasOverdueLoan;
    }

    public void UpdateStatus(MemberStatus newStatus)
    {
        Status = newStatus;
    }
}