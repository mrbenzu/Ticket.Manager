using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events.Events;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Events.EventSuspended;

public class EventSuspendedEventHandler(ISeatRepository seatRepository)  : IDomainEventHandler<EventSuspendedEvent>
{
    public async Task Handle(EventSuspendedEvent @event, CancellationToken cancellationToken)
    {
        var seats = await seatRepository.GetEventSeats(@event.EventId, cancellationToken);
        var enumerable = seats.ToList();
        if (enumerable.Count != 0)
        {
            foreach (var seat in enumerable)
            {
                seat.Suspend();
            }
        }
    }
}