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
        for (var sectorNumber = 1; sectorNumber <= domainEvent.SeatMap.SectorCount; sectorNumber++)
        {
            for (var rowNumber = 1; rowNumber <= domainEvent.SeatMap.RowsCount; rowNumber++)
            {
                for (var seatNumber = 1; seatNumber <= domainEvent.SeatMap.SeatsInRowCount; seatNumber++)
                {
                    var result = Seat.Create(domainEvent.EventId, true, sectorNumber, rowNumber, seatNumber);
                    if (result is { IsSuccess: true, Value: not null })
                        seats.Add(result.Value);
                }
            }
        }

        return seats;
    }
}