using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Seats.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Commands.ReturnSeat;

public class ReturnSeatCommandHandler(ISeatRepository seatRepository) : ICommandHandler<ReturnSeatCommand, Result>
{
    public async Task<Result> Handle(ReturnSeatCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);
        if (seat is null)
        {
            return Result.Failure(SeatApplicationErrors.SeatDoesntExist);
        }
        
        seat.Return();

        return Result.Success();
    }
}