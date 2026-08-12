using BuildingBlocks;
using BuildingBlocks.Extensions;
using BuildingBlocks.Messaging.Events;
using Loan.Application.Data;
using Loan.Domain.Entities;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.Logging;

namespace Loan.Application.Handlers.LoanRegistries.Commands.Create;

public class CreateLoanRegistryHandler(
    ILogger<CreateLoanRegistryHandler> logger,
    IApplicationDbContext context,
    IPublishEndpoint publishEndpoint)
    : IApplicationHandler<CreateLoanRegistryCommand, CreateLoanRegistryResult>
{
    public async Task<CreateLoanRegistryResult> Handle(CreateLoanRegistryCommand request, CancellationToken cancellationToken)
    {
        var loanRegistry = new LoanRegistry(
            request.loanRegistry.userId,
            request.loanRegistry.dueDate,
            request.loanRegistry.status);
        var items = request.loanRegistry.items.Select(x => new LoanItem(loanRegistry.Id, x.bookId)).ToList();
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
            loanRegistry.Items.Select(i => i.BookId).ToList());
        await publishEndpoint.Publish(loanRegistryCreatedEvent, cancellationToken);
    }
}