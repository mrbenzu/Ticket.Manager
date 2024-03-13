using FluentValidation;

namespace Ticket.Manager.Application.Events.ChangeName;

public class ChangeNameCommandValidator : AbstractValidator<ChangeNameCommand>
{
    public ChangeNameCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("EventId is required.");
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("A new event name is required.");
        
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage("Maximum length for event name is 100 char.");
    }
}