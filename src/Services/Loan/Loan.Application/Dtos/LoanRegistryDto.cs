using Loan.Domain.Enumerables;

namespace Loan.Application.Dtos;

public record LoanRegistryDto(
    Guid loanRegistryId,
    Guid userId,
    DateOnly loanDate,
    DateOnly dueDate,
    DateOnly? returnedDate,
    LoanRegistryStatus status,
    List<LoanItemDto> items);