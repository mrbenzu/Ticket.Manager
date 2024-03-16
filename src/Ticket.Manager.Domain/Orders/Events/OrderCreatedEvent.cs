using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Orders.Events;

public record OrderCreatedEvent(Guid OrderId, Guid UserId, IEnumerable<Guid> SeatIds) : DomainEvent;