using Loan.Domain.ValueObjects;

namespace Loan.Domain.Entities;

public class LoanItem
{
    public LoanItemId Id { get; private init; }

    public LoanRegistryId LoanRegistryId { get; private init; }

    public Guid BookId { get; private init; }

    public int Quantity { get; private init; }

    public LoanRegistry LoanRegistry { get; private init; } = null!;

    public LoanItem(
        LoanRegistryId loanRegistryId,
        Guid bookId,
        int quantity)
    {
        Id = new LoanItemId(Guid.NewGuid());
        LoanRegistryId = loanRegistryId;
        BookId = bookId;
        Quantity = quantity;
    }
}
