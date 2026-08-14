using BuildingBlocks.Messaging.Abstractions;

namespace BuildingBlocks.Messaging.Events;

public record LoanRegistryReturnedEvent(
    Guid loanRegistryId,
    Guid userId,
    DateOnly loanDate,
    DateOnly dueDate,
    DateOnly? returnedDate,
    string status,
    IReadOnlyCollection<LoanItemEventDto> items) : IEvent;