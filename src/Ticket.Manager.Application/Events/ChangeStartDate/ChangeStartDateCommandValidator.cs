using FluentValidation;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.ChangeStartDate;

public class ChangeStartDateCommandValidator : AbstractValidator<ChangeStartDateCommand>
{
    public ChangeStartDateCommandValidator() 
    {
        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("EventId is required.");
        
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required.");
        
        RuleFor(x => x.StartDate)
            .GreaterThan(SystemClock.Now)
            .WithMessage("Start date must be in the future.");
    }
}