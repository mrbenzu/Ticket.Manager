using MediatR;
using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Seats.Commands.ExtendReservationTime;
using Ticket.Manager.Domain.Orders.Events;

namespace Ticket.Manager.Application.Seats.Events.OrderCreated;

public class OrderCreatedEventHandler(ISender mediator) : IDomainEventHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent @event, CancellationToken cancellationToken)
    {
        foreach (var seatId in @event.SeatIds)
        {
            await mediator.Send(new ExtendReservationTimeCommand(seatId, @event.UserId), cancellationToken);
        }
    }
}