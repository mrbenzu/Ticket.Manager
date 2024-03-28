namespace Ticket.Manager.Domain.Places;

public interface IPlaceRepository
{
    void Add(Place place);
    Task<Place> Get(Guid id, CancellationToken cancellationToken);
}