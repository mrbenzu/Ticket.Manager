using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events.Events;

public record EventCreatedEvent(Guid EventId, UnnumberedSeatsMap UnnumberedSeatsMap, SeatsMap SeatsMap) : DomainEvent;