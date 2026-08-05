using Loan.Domain.ValueObjects;

namespace Loan.Domain.Entities;

public class LoanItem
{
    public LoanRegistryId LoanRegistryId { get; private init; }

    public Guid BookId { get; private init; }

    public LoanRegistry LoanRegistry { get; private init; } = null!;

    public LoanItem(
        LoanRegistryId loanRegistryId,
        Guid bookId)
    {
        LoanRegistryId = loanRegistryId;
        BookId = bookId;
    }
}
