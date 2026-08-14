using BuildingBlocks.Messaging.Abstractions;

namespace BuildingBlocks.Messaging.Events;

public record MemberHasOverdueLoanEvent(Guid userId) : IEvent;