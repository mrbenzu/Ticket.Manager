using FluentValidation;

namespace Ticket.Manager.Application.Seats.Commands.ReturnSeat;

public class ReturnSeatCommandValidator : AbstractValidator<ReturnSeatCommand>
{
    public ReturnSeatCommandValidator()
    {
        RuleFor(x => x.SeatId)
            .NotEmpty()
            .WithMessage("SeatId is required.");
        
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");
    }
}