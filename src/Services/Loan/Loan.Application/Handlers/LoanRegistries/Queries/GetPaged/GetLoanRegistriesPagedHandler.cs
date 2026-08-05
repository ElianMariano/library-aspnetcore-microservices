using BuildingBlocks;
using Loan.Application.Data;
using Loan.Application.Dtos;
using Loan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loan.Application.Handlers.LoanRegistries.Queries.GetPaged;

public class GetLoanRegistriesPagedHandler(
    IApplicationDbContext context)
    : IApplicationHandler<GetLoanRegistriesPagedQuery, GetLoanRegistriesPagedResult>
{
    public async Task<GetLoanRegistriesPagedResult> Handle(GetLoanRegistriesPagedQuery request, CancellationToken cancellationToken)
    {
        IQueryable<LoanRegistry> query = context.LoanRegistries.AsNoTracking();
        int totalItems = await query.CountAsync(cancellationToken);
        var loanRegistries = await query
            .Skip((request.CurrentPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var data = loanRegistries.Select(loanRegistry => new LoanRegistryDto(
            loanRegistry.Id.Value,
            loanRegistry.UserId,
            loanRegistry.LoanDate,
            loanRegistry.DueDate,
            loanRegistry.ReturnedDate,
            loanRegistry.Status,
            GetLoanItems(loanRegistry))).ToList();
        return new GetLoanRegistriesPagedResult(data, totalItems, request.CurrentPage, request.PageSize);
    }

    private List<LoanItemDto> GetLoanItems(LoanRegistry loanRegistry)
    {
        return loanRegistry.Items.Select(item => new LoanItemDto(
                item.LoanRegistryId.Value,
                item.BookId)).ToList();
    }
}