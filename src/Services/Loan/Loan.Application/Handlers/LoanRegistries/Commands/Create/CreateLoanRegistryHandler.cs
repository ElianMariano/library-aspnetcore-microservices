using BuildingBlocks;
using BuildingBlocks.Extensions;
using BuildingBlocks.Messaging.Events;
using Loan.Application.Data;
using Loan.Application.Exceptions;
using Loan.Application.Services;
using Loan.Domain.Entities;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Loan.Application.Handlers.LoanRegistries.Commands.Create;

public class CreateLoanRegistryHandler(
    ILogger<CreateLoanRegistryHandler> logger,
    IApplicationDbContext context,
    IPublishEndpoint publishEndpoint,
    IInventoryService inventoryService,
    IMembershipService membershipService)
    : IApplicationHandler<CreateLoanRegistryCommand, CreateLoanRegistryResult>
{
    public async Task<CreateLoanRegistryResult> Handle(CreateLoanRegistryCommand request, CancellationToken cancellationToken)
    {
        await ValidateMember(request, cancellationToken);
        await ValidateInventory(request, cancellationToken);
        var loanRegistry = new LoanRegistry(
            request.loanRegistry.userId,
            request.loanRegistry.dueDate,
            request.loanRegistry.status);
        var items = request.loanRegistry.items.Select(x => new LoanItem(loanRegistry.Id, x.bookId, x.quantity)).ToList();
        loanRegistry.AddItems(items);
        await context.LoanRegistries.AddAsync(loanRegistry, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogCreateInformation(loanRegistry.Id!.Value);
        await publishEvent(loanRegistry, cancellationToken);
        return new CreateLoanRegistryResult(loanRegistry.Id!.Value);
    }

    private async Task publishEvent(LoanRegistry loanRegistry, CancellationToken cancellationToken)
    {
        var loanRegistryCreatedEvent = new LoanRegistryCreatedEvent(
            loanRegistry.Id!.Value,
            loanRegistry.UserId,
            loanRegistry.LoanDate,
            loanRegistry.DueDate,
            loanRegistry.ReturnedDate,
            loanRegistry.Status.ToString(),
            loanRegistry.Items.Select(i => new LoanItemEventDto(i.BookId, i.Quantity)).ToList());
        await publishEndpoint.Publish(loanRegistryCreatedEvent, cancellationToken);
    }

    private async Task ValidateInventory(CreateLoanRegistryCommand request, CancellationToken cancellationToken)
    {
        var tasks = request.loanRegistry.items
            .Select(item =>
                inventoryService.CheckStock(
                    new InventoryServiceRequest(item.bookId, item.quantity)));

        var responses = await Task.WhenAll(tasks);

        foreach (var (item, response) in request.loanRegistry.items.Zip(responses))
        {
            if (response.available == false)
            {
                throw new BookUnavailableToLoanException(item.bookId);
            }
        }
    }

    private async Task ValidateMember(CreateLoanRegistryCommand request, CancellationToken cancellationToken)
    {
        int bookQuantity = request.loanRegistry.items.Sum(x => x.quantity);
        var response = await membershipService.CanMakeLoan(new MembershipServiceRequest(request.loanRegistry.userId, bookQuantity));
        if (response.ableToLoan == false)
        {
            throw new MemberNotAllowedToLoanException(request.loanRegistry.userId);
        }
    }
}