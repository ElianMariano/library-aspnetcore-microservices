using BuildingBlocks.Messaging.Events;
using Inventory.Application.Data;
using Inventory.Application.Exceptions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.EventHandlers;

public class LoanRegistryReturnedEventHandler(IApplicationDbContext dbContext, ILogger<LoanRegistryReturnedEventHandler> logger) : IConsumer<LoanRegistryReturnedEvent>
{
    public async Task Consume(ConsumeContext<LoanRegistryReturnedEvent> context)
    {
        logger.LogInformation("Inventory consumed event for loan registry {0}", context.Message.loanRegistryId);
        await UpdateBookInventory(context);
        // TODO: Right now , we are deleting the reservation when the book is returned, but we could keep it and change its status to "returned" or "completed" instead of deleting it. This would allow us to keep a record of the reservation history for future reference.
        await DeleteReservation(context);
    }

    public async Task UpdateBookInventory(ConsumeContext<LoanRegistryReturnedEvent> context)
    {
        foreach (Guid item in context.Message.items)
        {
            var bookInventory = await dbContext.BookInventories.FirstOrDefaultAsync(bookInventory => bookInventory.BookId == item);
            if (bookInventory == null)
            {
                throw new BookInventoryNotFoundException(item);
            }
            bookInventory.ReturnCopies();
        }
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }

    public async Task DeleteReservation(ConsumeContext<LoanRegistryReturnedEvent> context)
    {
        foreach (Guid item in context.Message.items)
        {
            var reservation = await dbContext.Reservations.FirstOrDefaultAsync(reservation => reservation.BookId == item);
            if (reservation != null)
            {
                dbContext.Reservations.Remove(reservation);
            }
        }
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }
}
