using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events.Events;

public record EventCreatedDomainEvent(Guid EventId, UnnumberedSeatsMap UnnumberedSeatsMap, SeatsMap SeatsMap) : DomainEvent;