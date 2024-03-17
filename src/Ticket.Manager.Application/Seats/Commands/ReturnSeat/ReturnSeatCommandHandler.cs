using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Commands.ReturnSeat;

public class ReturnSeatCommandHandler(ISeatRepository seatRepository) : ICommandHandler<ReturnSeatCommand>
{
    public async Task Handle(ReturnSeatCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);
        
        seat.Return();
    }
}