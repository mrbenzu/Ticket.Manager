using MediatR;
using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Seats.Commands.ReturnSeat;
using Ticket.Manager.Domain.Orders.Events;

namespace Ticket.Manager.Application.Seats.Events.OrderReturned;

public class OrderReturnedEventHandler(ISender mediator) : IDomainEventHandler<OrderReturnedEvent>
{
    public async Task Handle(OrderReturnedEvent @event, CancellationToken cancellationToken)
    {
        foreach (var seatId in @event.SeatIds)
        {
            await mediator.Send(new ReturnSeatCommand(seatId, @event.UserId), cancellationToken);
        }
    }
}