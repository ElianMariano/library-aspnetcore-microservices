using BuildingBlocks;
using BuildingBlocks.Extensions;
using Loan.Application.Data;
using Loan.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Loan.Application.Handlers.LoanRegistries.Commands.Create;

public class CreateLoanRegistryHandler(
    ILogger<CreateLoanRegistryHandler> logger,
    IApplicationDbContext context)
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
        return new CreateLoanRegistryResult(loanRegistry.Id!.Value);
    }
}