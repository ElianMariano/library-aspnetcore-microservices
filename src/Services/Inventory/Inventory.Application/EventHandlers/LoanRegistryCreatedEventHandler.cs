using BuildingBlocks.Messaging.Events;
using Inventory.Application.Data;
using Inventory.Application.Exceptions;
using Inventory.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.EventHandlers;

public class LoanRegistryCreatedEventHandler(IApplicationDbContext dbContext) : IConsumer<LoanRegistryCreatedEvent>
{
    public async Task Consume(ConsumeContext<LoanRegistryCreatedEvent> context)
    {
        await UpdateBookInventory(context);
        await CreateReservation(context);
    }

    public async Task UpdateBookInventory(ConsumeContext<LoanRegistryCreatedEvent> context)
    {
        // TODO: Right now we are updating all items inside a foreach loop, but we can optimize this by using a single query to update all items at once. This will reduce the number of database calls and improve performance.
        foreach (Guid item in context.Message.items)
        {
            var bookInventory = await dbContext.BookInventories.FirstOrDefaultAsync(bookInventory => bookInventory.BookId == item);
            if (bookInventory == null)
            {
                throw new BookInventoryNotFoundException(item);
            }
            bookInventory.ReserveCopies();
        }
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }

    public async Task CreateReservation(ConsumeContext<LoanRegistryCreatedEvent> context)
    {
        foreach (Guid item in context.Message.items)
        {
            // Assuming each item represents a single copy, you can set the quantity to 1. If you have a different logic for determining the quantity, you can adjust this accordingly.
            var reservation = new Reservation(
                item,
                context.Message.userId,
                1,
                context.Message.dueDate);
            await dbContext.Reservations.AddAsync(reservation);
        }
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }
}