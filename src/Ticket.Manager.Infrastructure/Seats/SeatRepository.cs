using Microsoft.EntityFrameworkCore;
using Ticket.Manager.Domain.Seats;
using Ticket.Manager.Infrastructure.Configuration;

namespace Ticket.Manager.Infrastructure.Seats;

public class SeatRepository(TicketManagerDbContext context) : ISeatRepository
{
    public void AddMany(IEnumerable<Seat> seats, CancellationToken cancellationToken) =>
        context.Seats.AddRange(seats);
    
    public async Task<Seat> Get(Guid id, CancellationToken cancellationToken) =>
        await context.Seats.SingleAsync(x => x.Id == id, cancellationToken: cancellationToken);

    public async Task<List<Seat>> GetEventSeats(Guid eventId, CancellationToken cancellationToken) =>
        await context.Seats.Where(x => x.EventId == eventId).ToListAsync(cancellationToken);

    public async Task<List<int>> GetReservedSeatNumbersInRow(Guid eventId, 
        int sectorNumber, int rowNumber,
        CancellationToken cancellationToken) =>
        await context.Seats
            .Where(x =>
                x.EventId == eventId && 
                x.SeatDetails.Sector == sectorNumber && 
                x.SeatDetails.RowNumber == rowNumber)
            .Select(x => x.SeatDetails.SeatNumber).ToListAsync(cancellationToken);

    public async Task<Seat?> GetFirstFreeSeatInRow(Guid eventId,
        int sectorNumber, int rowNumber, int seatNumber,
        CancellationToken cancellationToken) =>
        await context.Seats
            .FirstOrDefaultAsync(x =>
                x.EventId == eventId &&
                x.SeatDetails.Sector == sectorNumber &&
                x.SeatDetails.RowNumber == rowNumber &&
                x.IsReserved == false, cancellationToken);
}