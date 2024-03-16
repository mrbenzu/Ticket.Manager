using FluentValidation;

namespace Ticket.Manager.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator  : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(v => v.EventId)
            .NotEmpty()
            .WithMessage("EventId is required.");

        RuleFor(v => v.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");
        
        RuleFor(v => v.Seats)
            .NotEmpty()
            .WithMessage("Seats field cannot be empty.")
            .Must(seats => seats.Count > 0)
            .WithMessage("Must provide at least one seat for the order.");

        RuleForEach(v => v.Seats).ChildRules(seat =>
        {
            seat.RuleFor(s => s.SeatId)
                .NotEmpty()
                .WithMessage("SeatId is required.");
            
            seat.RuleFor(s => s.Price)
                .GreaterThan(0)
                .WithMessage("Price should be greater than 0.");
        });
    }
}