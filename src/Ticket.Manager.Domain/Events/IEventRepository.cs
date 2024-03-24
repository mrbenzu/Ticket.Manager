namespace Ticket.Manager.Domain.Events;

public interface IEventRepository
{
    void Add(Event @event, CancellationToken cancellationToken);
    Task<Event?> Get(Guid id, CancellationToken cancellationToken);
}