namespace Ticket.Manager.Domain.Events;

public interface IEventRepository
{
    void Add(Event @event);
    Task<Event> Get(Guid id, CancellationToken cancellationToken);
}