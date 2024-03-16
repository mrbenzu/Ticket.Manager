using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Orders;

namespace Ticket.Manager.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository) : ICommandHandler<CreateOrderCommand, Result>
{
    public async Task<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var seats = command.Seats.Select(x => Domain.Orders.Seat.Create(x.SeatId, x.Price).Value).ToList();
        var result = Order.Create(command.UserId, command.EventId, seats);

        if (result is not { IsSuccess: true, Value: not null }) 
            return Result.Failure(result.Error);
        
        await orderRepository.Add(result.Value, cancellationToken);
        
        return Result.Success();
    }
}