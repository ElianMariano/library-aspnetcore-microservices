namespace Inventory.Application.Exceptions;

public sealed class ReservationNotFoundException : ApplicationException
{
    public ReservationNotFoundException(Guid reservationId) : base("Reservation not found", reservationId)
    {
    }
}