using Microsoft.EntityFrameworkCore;
using Ticket.Manager.Domain.Events;
using Ticket.Manager.Infrastructure.Configuration;

namespace Ticket.Manager.Infrastructure.Events;

public class EventRepository(TicketManagerDbContext context) : IEventRepository
{
    public void Add(Event @event, CancellationToken cancellationToken) =>
        context.Events.Add(@event);

    public async Task<Event?> Get(Guid id, CancellationToken cancellationToken) =>
        await context.Events.SingleAsync(e => e.Id == id, cancellationToken);
}