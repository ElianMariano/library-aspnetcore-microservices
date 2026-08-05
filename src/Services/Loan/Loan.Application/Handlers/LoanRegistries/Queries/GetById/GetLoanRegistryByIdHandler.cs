using BuildingBlocks;
using Loan.Application.Data;
using Loan.Application.Dtos;
using Loan.Application.Exceptions;
using Loan.Domain.ValueObjects;

namespace Loan.Application.Handlers.LoanRegistries.Queries.GetById;

public class GetLoanRegistryByIdHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetLoanRegistryByIdQuery, GetLoanRegistryByIdResult>
{
    public async Task<GetLoanRegistryByIdResult> Handle(GetLoanRegistryByIdQuery request, CancellationToken cancellationToken)
    {
        var loanRegistryId = new LoanRegistryId(request.loanRegistryId);
        var loanRegistry = await context.LoanRegistries.FindAsync([loanRegistryId], cancellationToken: cancellationToken);
        if (loanRegistry is null)
        {
            throw new LoanRegistryNotFoundException(request.loanRegistryId);
        }
        var items = loanRegistry.Items.Select(item =>
            new LoanItemDto(
                item.LoanRegistryId.Value,
                item.BookId)).ToList();
        var data = new LoanRegistryDto(
            loanRegistry.Id.Value,
            loanRegistry.UserId,
            loanRegistry.LoanDate,
            loanRegistry.DueDate,
            loanRegistry.ReturnedDate,
            loanRegistry.Status,
            items);
        return new GetLoanRegistryByIdResult(data);
    }
}