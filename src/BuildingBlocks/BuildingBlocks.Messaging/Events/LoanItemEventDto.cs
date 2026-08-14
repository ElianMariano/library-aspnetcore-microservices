namespace BuildingBlocks.Messaging.Events;

public record LoanItemEventDto(Guid bookId, int quantity);