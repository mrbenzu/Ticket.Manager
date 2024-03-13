using FluentValidation;

namespace Ticket.Manager.Application.Seats.Commands.CancelReservation;

public class CancelReservationCommandValidator  : AbstractValidator<CancelReservationCommand>
{
    public CancelReservationCommandValidator()
    {
        RuleFor(x => x.SeatId)
            .NotEmpty()
            .WithMessage("SeatId is required.");
    }
}