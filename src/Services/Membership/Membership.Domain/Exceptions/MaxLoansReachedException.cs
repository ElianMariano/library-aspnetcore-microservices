namespace Membership.Domain.Exceptions;

public sealed class MaxLoansReachedException : ApplicationException
{
    public MaxLoansReachedException() : base("Max loans reached for member")
    {
    }
}