using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Orders;
using Ticket.Manager.Domain.Orders.Events;

namespace Ticket.Manager.Application.Orders.Events.PaymentRejected;

public class PaymentRejectedEventHandler(IOrderRepository orderRepository) : IDomainEventHandler<PaymentRejectedEvent>
{
    public async Task Handle(PaymentRejectedEvent @event, CancellationToken cancellationToken)
    {
        var order = await orderRepository.Get(@event.OrderId, cancellationToken);
        
        order?.Cancel();
    }
}