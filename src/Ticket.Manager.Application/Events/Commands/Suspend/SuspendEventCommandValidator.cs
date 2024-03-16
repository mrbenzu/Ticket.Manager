using FluentValidation;

namespace Ticket.Manager.Application.Events.Commands.Suspend;

public class SuspendEventCommandValidator  : AbstractValidator<SuspendEventCommand>
{
    public SuspendEventCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("Event Id is required.");
    }
}