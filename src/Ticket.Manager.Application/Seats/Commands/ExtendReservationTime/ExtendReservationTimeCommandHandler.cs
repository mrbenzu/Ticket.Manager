using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Commands.ExtendReservationTime;

public class ExtendReservationTimeCommandHandler(ISeatRepository seatRepository) : ICommandHandler<ExtendReservationTimeCommand>
{
    public async Task Handle(ExtendReservationTimeCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);

        seat.ExtendReservationTime(command.UserId);
    }
}