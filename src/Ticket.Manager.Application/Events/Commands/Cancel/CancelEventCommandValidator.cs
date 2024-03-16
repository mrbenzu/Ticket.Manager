using FluentValidation;

namespace Ticket.Manager.Application.Events.Commands.Cancel;

public class CancelEventCommandValidator : AbstractValidator<CancelEventCommand>
{
    public CancelEventCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("EventId is required.");
    }
}