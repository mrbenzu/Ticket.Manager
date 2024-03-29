﻿using FluentValidation;

namespace Ticket.Manager.Application.Seats.Commands.ExtendReservationTime;

public class ExtendReservationTimeCommandValidator : AbstractValidator<ExtendReservationTimeCommand>
{
    public ExtendReservationTimeCommandValidator()
    {
        RuleFor(x => x.SeatId)
            .NotEmpty()
            .WithMessage("SeatId is required.");
        
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");
    }
}