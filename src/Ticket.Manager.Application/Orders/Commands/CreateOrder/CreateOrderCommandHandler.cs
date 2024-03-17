using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Orders;

namespace Ticket.Manager.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository) : ICommandHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var seats = command.Seats.Select(x => Domain.Orders.Seat.Create(x.SeatId, x.Price)).ToList();
        var result = Order.Create(command.UserId, command.EventId, seats);

        
        await orderRepository.Add(result, cancellationToken);
    }
}