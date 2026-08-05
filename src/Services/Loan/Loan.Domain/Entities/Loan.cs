using Loan.Domain.Enumerables;
using Loan.Domain.ValueObjects;

namespace Loan.Domain.Entities;

public class Loan
{
    public LoanId Id { get; private init; }

    public Guid UserId { get; private init; }

    public DateOnly LoanDate { get; private init; }

    public DateOnly DueDate { get; private set; }

    public DateOnly? ReturnedDate { get; private set; }

    public LoanStatus Status { get; private set; }

    public IReadOnlyCollection<LoanItem> Items { get; private set; } = null!;

    public Loan(
        Guid userId,
        DateOnly dueDate,
        LoanStatus status)
    {
        Id = new LoanId(Guid.NewGuid());
        UserId = userId;
        LoanDate = DateOnly.FromDateTime(DateTime.Now);
        DueDate = dueDate;
        Status = status;
    }

    public void ChangeStatus(LoanStatus status)
    {
        Status = status;
    }

    public void ReturnLoan()
    {
        ReturnedDate = DateOnly.FromDateTime(DateTime.Now);
        Status = LoanStatus.Returned;
    }
}