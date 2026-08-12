namespace Inventory.Domain.Exceptions;

public sealed class UnavalilableCopiesToReserveException : DomainException
{
    public UnavalilableCopiesToReserveException() : base("Not enough available copies to reserve.")
    {
    }
}