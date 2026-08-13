namespace Loan.Application.Exceptions;

public sealed class BookUnavailableToLoanException : ApplicationException
{
    public BookUnavailableToLoanException(Guid bookId) : base("Book unavailable to loan", bookId)
    {
    }
}