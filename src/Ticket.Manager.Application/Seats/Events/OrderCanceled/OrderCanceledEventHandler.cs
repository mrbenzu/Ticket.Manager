using MediatR;
using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Seats.Commands.CancelReservation;
using Ticket.Manager.Domain.Orders.Events;

namespace Ticket.Manager.Application.Seats.Events.OrderCanceled;

public class OrderCanceledEventHandler(ISender mediator) : IDomainEventHandler<OrderCanceledEvent>
{
    public async Task Handle(OrderCanceledEvent @event, CancellationToken cancellationToken)
    {
        foreach (var seatId in @event.SeatIds)
        {
            await mediator.Send(new CancelReservationCommand(seatId, @event.UserId), cancellationToken);
        }
    }
}