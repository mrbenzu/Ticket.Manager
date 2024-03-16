using FluentValidation;

namespace Ticket.Manager.Application.Seats.Commands.Reserve;

public class ReserveSeatCommandValidator : AbstractValidator<ReserveSeatCommand>
{
    public ReserveSeatCommandValidator()
    {
        RuleFor(x => x.SeatId)
            .NotEmpty()
            .WithMessage("SeatId is required.");
        
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");
    }
}