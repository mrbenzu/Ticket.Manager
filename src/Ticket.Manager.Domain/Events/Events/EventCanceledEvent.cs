using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events.Events;

public record EventCanceledEvent(Guid EventId) : DomainEvent;