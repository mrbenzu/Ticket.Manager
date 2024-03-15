using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Orders.Events;

public record OrderCreatedEvent(Guid OrderId, IEnumerable<Guid> SeatIds) : DomainEvent;