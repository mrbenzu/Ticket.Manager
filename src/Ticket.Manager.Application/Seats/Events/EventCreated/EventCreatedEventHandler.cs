﻿using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events.Events;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Events.EventCreated;

public class EventCreatedEventHandler(ISeatRepository seatRepository) : IDomainEventHandler<EventCreatedEvent>
{
    public Task Handle(EventCreatedEvent @event, CancellationToken cancellationToken)
    {
        var seats = GenerateSeats(@event);

        seatRepository.AddMany(seats);
        
        return Task.CompletedTask;
    }

    private static IEnumerable<Seat> GenerateSeats(EventCreatedEvent @event)
    {
        var seats = new List<Seat>();

        for (var sectorNumber = 1; sectorNumber <= @event.UnnumberedSeatsMap.SectorCount; sectorNumber++)
        {
            for (var seatNumber = 1; seatNumber <= @event.UnnumberedSeatsMap.SeatsInSectorCount; seatNumber++)
            {
                seats.Add(Seat.Create(@event.EventId, true, sectorNumber, 0, seatNumber));
            }
        }

        for (var sectorNumber = 1; sectorNumber <= @event.SeatsMap.SectorCount; sectorNumber++)
        {
            for (var rowNumber = 1; rowNumber <= @event.SeatsMap.RowsCount; rowNumber++)
            {
                for (var seatNumber = 1; seatNumber <= @event.SeatsMap.SeatsInRowCount; seatNumber++)
                {
                    seats.Add(Seat.Create(@event.EventId, false, sectorNumber, rowNumber, seatNumber));
                }
            }
        }

        return seats;
    }
}