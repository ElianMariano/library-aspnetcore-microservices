namespace Membership.Application.Exceptions;

public sealed class MemberNotFoundException : ApplicationException
{
    public MemberNotFoundException(Guid memberId) : base("Member not found", memberId)
    {
    }
}