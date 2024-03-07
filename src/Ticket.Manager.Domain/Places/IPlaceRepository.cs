namespace Ticket.Manager.Domain.Places;

public interface IPlaceRepository
{
    Task Add(Place place, CancellationToken cancellationToken);
    Task<Place?> Get(long id, CancellationToken cancellationToken);
}