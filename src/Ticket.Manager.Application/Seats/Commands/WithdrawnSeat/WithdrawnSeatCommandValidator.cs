using FluentValidation;

namespace Ticket.Manager.Application.Seats.Commands.WithdrawnSeat;

public class WithdrawnSeatCommandValidator  : AbstractValidator<WithdrawnSeatCommand>
{
    public WithdrawnSeatCommandValidator()
    {
        RuleFor(x => x.SeatId)
            .NotEmpty()
            .WithMessage("SeatId is required.");
    }
}