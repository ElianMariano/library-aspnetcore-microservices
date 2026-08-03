using Catalog.Domain.Abstractions;
using Catalog.Domain.Entities;

namespace Catalog.Domain.Events;

public record BookUpdatedEvent(Book book) : IEvent;