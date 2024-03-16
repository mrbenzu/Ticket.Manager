﻿using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Seats.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Seats;

namespace Ticket.Manager.Application.Seats.Commands.SellSeat;

public class SellSeatCommandHandler(ISeatRepository seatRepository) : ICommandHandler<SellSeatCommand, Result>
{
    public async Task<Result> Handle(SellSeatCommand command, CancellationToken cancellationToken)
    {
        var seat = await seatRepository.Get(command.SeatId, cancellationToken);
        if (seat is null)
        {
            return Result.Failure(SeatApplicationErrors.SeatDoesntExist);
        }

        var result = seat.Sell(command.UserId);

        return result;
    }
}