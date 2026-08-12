using BuildingBlocks.Messaging.Abstractions;

namespace BuildingBlocks.Messaging.Events;

public record LoanRegistryCreatedEvent(
    Guid loanRegistryId,
    Guid userId,
    DateOnly loanDate,
    DateOnly dueDate,
    DateOnly? returnedDate,
    string status,
    IReadOnlyCollection<Guid> items) : IEvent;