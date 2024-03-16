using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.Events;

public record SeatWithdrawnEvent(Guid SeatId, Guid EventId, Guid UserId, 
    bool WasReserved, bool WasNumerable, int SectorNumber, int RowNumber, int SeatNumber) : DomainEvent;