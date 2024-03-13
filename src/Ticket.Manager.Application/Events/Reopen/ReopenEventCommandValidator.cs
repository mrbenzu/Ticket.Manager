using FluentValidation;

namespace Ticket.Manager.Application.Events.Reopen;

public class ReopenEventCommandValidator  : AbstractValidator<ReopenEventCommand>
{
    public ReopenEventCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage("Event Id is required.");
    }
}