using FluentValidation;

namespace Ticket.Manager.Application.Orders.Commands.ReturnOrder;

public class ReturnOrderCommandValidator  : AbstractValidator<ReturnOrderCommand>
{
    public ReturnOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId is required.");
        
        RuleFor(v => v.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");
    }
}