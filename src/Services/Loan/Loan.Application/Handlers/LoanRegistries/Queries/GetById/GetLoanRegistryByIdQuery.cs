using BuildingBlocks.DataTransferObjects;
using Loan.Application.Dtos;

namespace Loan.Application.Handlers.LoanRegistries.Queries.GetById;

public record GetLoanRegistryByIdQuery(Guid loanRegistryId);

public sealed class GetLoanRegistryByIdResult(LoanRegistryDto Data, int StatusCode = 200) : ResponseBase<LoanRegistryDto>(Data, StatusCode);