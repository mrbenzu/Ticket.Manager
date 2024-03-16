using FluentValidation;

namespace Ticket.Manager.Application.Seats.Commands.ExtendReservationTime;

public class ExtendReservationTimeCommandValidator : AbstractValidator<ExtendReservationTimeCommand>
{
    public ExtendReservationTimeCommandValidator()
    {
        RuleFor(x => x.SeatId)
            .NotEmpty()
            .WithMessage("SeatId is required.");
    }
}