using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Seats.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Commands.ExtendReservationTime;

public class ExtendReservationTimeCommandHandler(ISeatRepository seatRepository) : ICommandHandler<ExtendReservationTimeCommand, Result>
{
    public async Task<Result> Handle(ExtendReservationTimeCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);
        if (seat is null)
        {
            return Result.Failure(SeatApplicationErrors.SeatDoesntExist);
        }

        var result = seat.ExtendReservationTime(command.UserId);

        return result;
    }
}