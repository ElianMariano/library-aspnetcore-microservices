namespace Loan.Domain.Entities;

public class LoanItem
{
    public Guid LoanId { get; private init; }

    public Guid BookId { get; private init; }

    public LoanItem(
        Guid loanId,
        Guid bookId)
    {
        LoanId = loanId;
        BookId = bookId;
    }
}
