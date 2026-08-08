using Loan.Domain.Enumerables;
using Loan.Domain.ValueObjects;

namespace Loan.Domain.Entities;

public class LoanRegistry
{
    public LoanRegistryId Id { get; private init; }

    public Guid UserId { get; private init; }

    public DateOnly LoanDate { get; private init; }

    public DateOnly DueDate { get; private set; }

    public DateOnly? ReturnedDate { get; private set; }

    public LoanRegistryStatus Status { get; private set; }

    private List<LoanItem> _items = [];

    public IReadOnlyCollection<LoanItem> Items => _items.AsReadOnly();

    public LoanRegistry(
        Guid userId,
        DateOnly dueDate,
        LoanRegistryStatus status)
    {
        Id = new LoanRegistryId(Guid.NewGuid());
        UserId = userId;
        LoanDate = DateOnly.FromDateTime(DateTime.Now);
        DueDate = dueDate;
        Status = status;
    }

    public void ChangeStatus(LoanRegistryStatus status)
    {
        Status = status;
    }

    public void ReturnLoan()
    {
        ReturnedDate = DateOnly.FromDateTime(DateTime.Now);
        Status = LoanRegistryStatus.Returned;
    }

    public void AddItems(List<LoanItem> items)
    {
        _items = items;
    }

    public void AddItem(Guid bookId)
    {
        var item = new LoanItem(this.Id, bookId);
        _items.Add(item);
    }

    public void RemoveItem(Guid bookId)
    {
        var item = _items.FirstOrDefault(i => i.BookId == bookId && i.LoanRegistryId.Value == this.Id.Value);
        if (item != null)
        {
            _items.Remove(item);
        }
    }
}