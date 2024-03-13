using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.Events;

public record ReservationCanceled(Guid SeatId) : DomainEvent;