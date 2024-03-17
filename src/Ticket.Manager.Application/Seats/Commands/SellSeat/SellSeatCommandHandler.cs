using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Commands.SellSeat;

public class SellSeatCommandHandler(ISeatRepository seatRepository) : ICommandHandler<SellSeatCommand>
{
    public async Task Handle(SellSeatCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);

        seat.Sell(command.UserId);
    }
}