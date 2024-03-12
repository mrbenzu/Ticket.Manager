using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events.Events;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Events.EventCreated;

public class EventCreatedDomainEventHandler(ISeatRepository seatRepository) : IDomainEventHandler<EventCreatedDomainEvent>
{
    public async Task Handle(EventCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var seats = GenerateSeats(domainEvent);

        await seatRepository.AddMany(seats, cancellationToken);
    }

    private static IEnumerable<Seat> GenerateSeats(EventCreatedDomainEvent domainEvent)
    {
        var seats = new List<Seat>();

        for (var sectorNumber = 1; sectorNumber <= domainEvent.UnnumberedSeatsMap.SectorCount; sectorNumber++)
        {
            for (var seatNumber = 1; seatNumber <= domainEvent.UnnumberedSeatsMap.SeatsInSectorCount; seatNumber++)
            {
                var result = Seat.Create(domainEvent.EventId, true, sectorNumber, 0, seatNumber);
                if (result is { IsSuccess: true, Value: not null })
                {
                    seats.Add(result.Value);
                }
            }
        }

        for (var sectorNumber = 1; sectorNumber <= domainEvent.SeatsMap.SectorCount; sectorNumber++)
        {
            for (var rowNumber = 1; rowNumber <= domainEvent.SeatsMap.RowsCount; rowNumber++)
            {
                for (var seatNumber = 1; seatNumber <= domainEvent.SeatsMap.SeatsInRowCount; seatNumber++)
                {
                    var result = Seat.Create(domainEvent.EventId, false, sectorNumber, rowNumber, seatNumber);
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