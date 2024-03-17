using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Orders;

namespace Ticket.Manager.Application.Orders.Commands.ReturnOrder;

public class ReturnOrderCommandHandler(IOrderRepository orderRepository) : ICommandHandler<ReturnOrderCommand>
{
    public async Task Handle(ReturnOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await orderRepository.Get(command.OrderId, cancellationToken);

        order.Return(command.UserId);
    }
}