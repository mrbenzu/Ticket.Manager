namespace Ticket.Manager.Domain.Orders;

public interface IOrderRepository
{
    Task<Order> Get(Guid id, CancellationToken cancellationToken);
    
    Task Add(Order order, CancellationToken cancellationToken);
}