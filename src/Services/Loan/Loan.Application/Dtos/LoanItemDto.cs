namespace Loan.Application.Dtos;

public record LoanItemDto(Guid loanRegistryId, Guid bookId, int quantity);