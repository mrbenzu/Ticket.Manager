using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Orders.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Orders;

namespace Ticket.Manager.Application.Orders.Commands.ReturnOrder;

public class ReturnOrderCommandHandler(IOrderRepository orderRepository) : ICommandHandler<ReturnOrderCommand, Result>
{
    public async Task<Result> Handle(ReturnOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await orderRepository.Get(command.OrderId, cancellationToken);
        if (order is null)
        {
            return Result.Failure(OrderApplicationErrors.OrderDoesntExist);
        }

        var result = order.Return(command.UserId);

        return result;
    }
}