using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Domain.Places;

public interface ISeatRepository
{
    Task<Seat?> Get(Guid id, CancellationToken cancellationToken);
}