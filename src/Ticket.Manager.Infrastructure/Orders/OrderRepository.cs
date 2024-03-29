using Microsoft.EntityFrameworkCore;
using Ticket.Manager.Domain.Orders;
using Ticket.Manager.Infrastructure.Configuration;

namespace Ticket.Manager.Infrastructure.Orders;

public class OrderRepository(TicketManagerDbContext context) : IOrderRepository
{
    public void Add(Order order) =>
        context.Orders.Add(order);
    
    public async Task<Order> Get(Guid id, CancellationToken cancellationToken) =>
        await context.Orders.SingleAsync(x => x.Id == id, cancellationToken);
}