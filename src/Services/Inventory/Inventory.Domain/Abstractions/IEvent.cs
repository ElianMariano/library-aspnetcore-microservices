namespace Inventory.Domain.Abstractions;

public interface IEvent
{
    public Guid EventId => Guid.NewGuid();

    public DateTime OcurredAt => DateTime.UtcNow;
}