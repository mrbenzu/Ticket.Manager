using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events.Events;

public record EventCreatedDomainEvent(Guid EventId, SeatMap SeatMap) : DomainEvent;