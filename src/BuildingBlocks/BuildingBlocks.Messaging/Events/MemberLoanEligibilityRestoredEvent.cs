using BuildingBlocks.Messaging.Abstractions;

namespace BuildingBlocks.Messaging.Events;

public record MemberLoanEligibilityRestoredEvent(Guid userId) : IEvent;