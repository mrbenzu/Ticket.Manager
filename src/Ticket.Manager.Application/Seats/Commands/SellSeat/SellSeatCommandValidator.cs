using FluentValidation;

namespace Ticket.Manager.Application.Seats.Commands.SellSeat;

public class SellSeatCommandValidator : AbstractValidator<SellSeatCommand>
{
    public SellSeatCommandValidator()
    {
        RuleFor(x => x.SeatId)
            .NotEmpty()
            .WithMessage("SeatId is required.");
        
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");
    }
}