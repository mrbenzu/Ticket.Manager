using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.Events;

public record SeatReopenedEvent(Guid SeatId) : DomainEvent;