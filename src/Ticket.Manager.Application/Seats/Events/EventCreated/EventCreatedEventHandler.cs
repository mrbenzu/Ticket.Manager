using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events.Events;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Events.EventCreated;

public class EventCreatedEventHandler(ISeatRepository seatRepository) : IDomainEventHandler<EventCreatedEvent>
{
    public async Task Handle(EventCreatedEvent @event, CancellationToken cancellationToken)
    {
        var seats = GenerateSeats(@event);

        await seatRepository.AddMany(seats, cancellationToken);
    }

    private static IEnumerable<Seat> GenerateSeats(EventCreatedEvent @event)
    {
        var seats = new List<Seat>();

        for (var sectorNumber = 1; sectorNumber <= @event.UnnumberedSeatsMap.SectorCount; sectorNumber++)
        {
            for (var seatNumber = 1; seatNumber <= @event.UnnumberedSeatsMap.SeatsInSectorCount; seatNumber++)
            {
                var result = Seat.Create(@event.EventId, true, sectorNumber, 0, seatNumber);
                if (result is { IsSuccess: true, Value: not null })
                {
                    seats.Add(result.Value);
                }
            }
        }

        for (var sectorNumber = 1; sectorNumber <= @event.SeatsMap.SectorCount; sectorNumber++)
        {
            for (var rowNumber = 1; rowNumber <= @event.SeatsMap.RowsCount; rowNumber++)
            {
                for (var seatNumber = 1; seatNumber <= @event.SeatsMap.SeatsInRowCount; seatNumber++)
                {
                    var result = Seat.Create(@event.EventId, false, sectorNumber, rowNumber, seatNumber);
                    if (result is { IsSuccess: true, Value: not null })
                    {
                        seats.Add(result.Value);
                    }
                }
            }
        }

        return seats;
    }
}