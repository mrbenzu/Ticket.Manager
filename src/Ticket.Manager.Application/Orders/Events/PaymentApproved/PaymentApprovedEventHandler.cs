using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Orders;
using Ticket.Manager.Domain.Orders.Events;

namespace Ticket.Manager.Application.Orders.Events.PaymentApproved;

public class PaymentApprovedEventHandler(IOrderRepository orderRepository) : IDomainEventHandler<PaymentApprovedEvent>
{
    public async Task Handle(PaymentApprovedEvent @event, CancellationToken cancellationToken)
    {
        var order = await orderRepository.Get(@event.OrderId, cancellationToken);
        
        order.Approve();
    }
}