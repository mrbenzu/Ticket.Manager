namespace Ticket.Manager.Domain.Seats;

public interface ISeatRepository
{
    Task AddMany(IEnumerable<Seat> seats, CancellationToken cancellationToken);
    
    Task<Seat> Get(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Seat>> GetEventSeats(Guid eventId, CancellationToken cancellationToken);
    
    Task<IEnumerable<int>> GetReservedSeatNumbersInRow(Guid eventId, 
        int sectorNumber, int rowNumber, int seatNumber, CancellationToken cancellationToken);

    Task<Seat?> GetFirstFreeSeatInRow(Guid eventId, int sectorNumber, int rowNumber, int seatNumber, CancellationToken cancellationToken);
}