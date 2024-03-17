using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Commands.Reserve;

public class ReserveSeatCommandHandler(ISeatRepository seatRepository) : ICommandHandler<ReserveSeatCommand>
{
    public async Task Handle(ReserveSeatCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);
        var reservedSeatNumbersInRow = await seatRepository.GetReservedSeatNumbersInRow(seat.EventId, 
            seat.SeatDetails.Sector, seat.SeatDetails.RowNumber, seat.SeatDetails.SeatNumber, cancellationToken);
        
        seat.Reserve(command.UserId, reservedSeatNumbersInRow.ToList());
    }
}