namespace Ticket.Manager.Domain.Events;

public interface IEventRepository
{
    Task Add(Event @event, CancellationToken cancellationToken);
    Task<Event> Get(Guid id, CancellationToken cancellationToken);
}