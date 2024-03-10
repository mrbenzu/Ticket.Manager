using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Seats.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Places;

namespace Ticket.Manager.Application.Seats.WithdrawnSeat;

public class WithdrawnSeatCommandHandler(ISeatRepository seatRepository) : ICommandHandler<WithdrawnSeatCommand, Result>
{
    public async Task<Result> Handle(WithdrawnSeatCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);
        if (seat is null)
        {
            return Result.Failure(SeatApplicationErrors.SeatDoesntExist);
        }

        seat.Withdrawn();

        return Result.Success();
    }
}