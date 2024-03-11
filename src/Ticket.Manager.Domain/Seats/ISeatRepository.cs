namespace Ticket.Manager.Domain.Seats;

public interface ISeatRepository
{
    Task AddMany(IEnumerable<Seat> seats, CancellationToken cancellationToken);
    
    Task<Seat?> Get(Guid id, CancellationToken cancellationToken);
    
    Task<IEnumerable<int>> GetReservedSeatNumbersInRow(Guid eventId, SeatDetails seatDetails, CancellationToken cancellationToken);
}