using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Seats;
using Ticket.Manager.Domain.Seats.Events;

namespace Ticket.Manager.Application.Seats.Events.SeatWithdrawn;

public class SeatWithdrawnEventHandler(ISeatRepository seatRepository) : IDomainEventHandler<SeatWithdrawnEvent>
{
    public async Task Handle(SeatWithdrawnEvent @event, CancellationToken cancellationToken)
    {
        if (@event.WasReserved)
        {
            var newSeatForUser = await seatRepository.GetFirstFreeSeatInRow(@event.EventId, 
                @event.SectorNumber, @event.RowNumber, @event.SeatNumber, cancellationToken);

            newSeatForUser?.Reserve(@event.UserId, [], true);
        }
    }
}