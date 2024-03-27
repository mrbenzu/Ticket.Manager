namespace Ticket.Manager.Domain.Seats;

public interface ISeatRepository
{
    void AddMany(IEnumerable<Seat> seats, CancellationToken cancellationToken);
    
    Task<Seat> Get(Guid id, CancellationToken cancellationToken);

    Task<List<Seat>> GetEventSeats(Guid eventId, CancellationToken cancellationToken);
    
    Task<List<int>> GetReservedSeatNumbersInRow(Guid eventId, 
        int sectorNumber, int rowNumber, CancellationToken cancellationToken);

    Task<Seat?> GetFirstFreeSeatInRow(Guid eventId, int sectorNumber, int rowNumber, int seatNumber, CancellationToken cancellationToken);
}