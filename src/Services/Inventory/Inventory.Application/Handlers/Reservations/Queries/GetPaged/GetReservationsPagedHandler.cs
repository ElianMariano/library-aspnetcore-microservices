using BuildingBlocks;
using Inventory.Application.Data;
using Inventory.Application.Dtos;
using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.Handlers.Reservations.Queries.GetPaged;

public class GetReservationsPagedHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetReservationsPagedQuery, GetReservationsPagedResult>
{
    public async Task<GetReservationsPagedResult> Handle(GetReservationsPagedQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Reservation> query = context.Reservations.AsNoTracking();
        int totalItems = await query.CountAsync(cancellationToken);
        var reservations = await query
            .Skip((request.CurrentPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var data = reservations.Select(reservation => new ReservationDto(
            reservation.Id.Value,
            reservation.BookId,
            reservation.UserId,
            reservation.Quantity,
            reservation.ExpiresAt)).ToList();
        return new GetReservationsPagedResult(data, totalItems, request.CurrentPage, request.PageSize);
    }
}