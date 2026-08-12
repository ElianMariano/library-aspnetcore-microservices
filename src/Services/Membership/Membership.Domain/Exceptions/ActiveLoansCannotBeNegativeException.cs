namespace Membership.Domain.Exceptions;

public sealed class ActiveLoansCannotBeNegativeException : ApplicationException
{
    public ActiveLoansCannotBeNegativeException() : base("Active loans cannot be negative.")
    {
    }
}