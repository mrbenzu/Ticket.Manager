using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Commands.CancelReservation;

public class CancelReservationCommandHandler(ISeatRepository seatRepository) : ICommandHandler<CancelReservationCommand>
{
    public async Task Handle(CancelReservationCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);
        
        seat.CancelReservation(command.UserId);
    }
}