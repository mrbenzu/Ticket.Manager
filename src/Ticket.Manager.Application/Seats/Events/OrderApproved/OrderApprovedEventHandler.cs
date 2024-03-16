using MediatR;
using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Seats.Commands.SellSeat;
using Ticket.Manager.Domain.Orders.Events;

namespace Ticket.Manager.Application.Seats.Events.OrderApproved;

public class OrderApprovedEventHandler(ISender mediator) : IDomainEventHandler<OrderApprovedEvent>
{
    public async Task Handle(OrderApprovedEvent @event, CancellationToken cancellationToken)
    {
        foreach (var seatId in @event.SeatIds)
        {
            await mediator.Send(new SellSeatCommand(seatId, @event.UserId), cancellationToken);
        }
    }
}