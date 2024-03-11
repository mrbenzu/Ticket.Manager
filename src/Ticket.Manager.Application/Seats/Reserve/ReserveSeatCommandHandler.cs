using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Seats.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Reserve;

public class ReserveSeatCommandHandler(ISeatRepository seatRepository) : ICommandHandler<ReserveSeatCommand, Result>
{
    public async Task<Result> Handle(ReserveSeatCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);
        if (seat is null)
        {
            return Result.Failure(SeatApplicationErrors.SeatDoesntExist);
        }
        
        var reservedSeatNumbersInRow = await seatRepository.GetReservedSeatNumbersInRow(seat.EventId, seat.SeatDetails, cancellationToken);
        var result = seat.Reserve(command.UserId, reservedSeatNumbersInRow.ToList());
        
        return result;
    }
}