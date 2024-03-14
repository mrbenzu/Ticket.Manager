using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Seats.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Commands.CancelReservation;

public class CancelReservationCommandHandler(ISeatRepository seatRepository) : ICommandHandler<CancelReservationCommand, Result>
{
    public async Task<Result> Handle(CancelReservationCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);
        if (seat is null)
        {
            return Result.Failure(SeatApplicationErrors.SeatDoesntExist);
        }

        seat.CancelReservation(command.UserId);

        return Result.Success();
    }
}