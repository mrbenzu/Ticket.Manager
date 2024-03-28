using Microsoft.EntityFrameworkCore;
using Ticket.Manager.Domain.Places;
using Ticket.Manager.Infrastructure.Configuration;

namespace Ticket.Manager.Infrastructure.Places;

public class PlaceRepository(TicketManagerDbContext context) : IPlaceRepository
{
    public void Add(Place place) =>
        context.Places.Add(place);
    
    public async Task<Place> Get(Guid id, CancellationToken cancellationToken) =>
        await context.Places.SingleAsync(e => e.Id == id, cancellationToken);
}