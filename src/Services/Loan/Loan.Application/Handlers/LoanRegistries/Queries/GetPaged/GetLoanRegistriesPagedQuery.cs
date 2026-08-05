using BuildingBlocks.DataTransferObjects;
using Loan.Application.Dtos;

namespace Loan.Application.Handlers.LoanRegistries.Queries.GetPaged;

public sealed class GetLoanRegistriesPagedQuery(int currentPage = 1, int pageSize = 12) : PagedRequestBase(currentPage, pageSize);

public sealed class GetLoanRegistriesPagedResult(IReadOnlyList<LoanRegistryDto> Data, int TotalItems, int currentPage = 1, int pageSize = 12) : PagedResponseBase<LoanRegistryDto>(Data, TotalItems, currentPage, pageSize);