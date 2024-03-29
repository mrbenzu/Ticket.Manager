namespace Ticket.Manager.Domain.Orders;

public interface IOrderRepository
{
    void Add(Order order);

    Task<Order> Get(Guid id, CancellationToken cancellationToken);
}