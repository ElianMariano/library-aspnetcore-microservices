using BuildingBlocks.Messaging.Events;
using Inventory.Application.Data;
using Inventory.Application.Exceptions;
using Inventory.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.EventHandlers;

public class InventoryLoanRegistryCreatedEventHandler(IApplicationDbContext dbContext, ILogger<InventoryLoanRegistryCreatedEventHandler> logger) : IConsumer<LoanRegistryCreatedEvent>
{
    public async Task Consume(ConsumeContext<LoanRegistryCreatedEvent> context)
    {
        logger.LogInformation("Inventory consumed event for loan registry {0}", context.Message.loanRegistryId);
        await UpdateBookInventory(context);
        await CreateReservation(context);
    }

    public async Task UpdateBookInventory(ConsumeContext<LoanRegistryCreatedEvent> context)
    {
        foreach (var item in context.Message.items)
        {
            var bookInventory = await dbContext.BookInventories.FirstOrDefaultAsync(bookInventory => bookInventory.BookId == item.bookId);
            if (bookInventory == null)
            {
                throw new BookInventoryNotFoundException(item.bookId);
            }
            bookInventory.ReserveCopies(item.quantity);
        }
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }

    public async Task CreateReservation(ConsumeContext<LoanRegistryCreatedEvent> context)
    {
        foreach (var item in context.Message.items)
        {
            var reservation = new Reservation(
                item.bookId,
                context.Message.userId,
                item.quantity,
                context.Message.dueDate);
            await dbContext.Reservations.AddAsync(reservation);
        }
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }
}