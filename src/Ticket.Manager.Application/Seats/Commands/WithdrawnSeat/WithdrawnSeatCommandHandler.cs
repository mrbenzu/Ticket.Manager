using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Commands.WithdrawnSeat;

public class WithdrawnSeatCommandHandler(ISeatRepository seatRepository) : ICommandHandler<WithdrawnSeatCommand>
{
    public async Task Handle(WithdrawnSeatCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);

        seat.Withdrawn();
    }
}