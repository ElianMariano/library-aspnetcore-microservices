using Inventory.Application.Handlers.Reservations.Commands.Create;
using Inventory.Application.Handlers.Reservations.Commands.Delete;
using Inventory.Application.Handlers.Reservations.Queries.GetById;
using Inventory.Application.Handlers.Reservations.Queries.GetPaged;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application.Handlers.Reservations;

public static class IncludeReservationHandlers
{
    public static void Include(IServiceCollection builder)
    {
        builder.AddScoped<CreateReservationHandler>();
        builder.AddScoped<DeleteReservationHandler>();
        builder.AddScoped<GetReservationByIdHandler>();
        builder.AddScoped<GetReservationsPagedHandler>();
    }
}