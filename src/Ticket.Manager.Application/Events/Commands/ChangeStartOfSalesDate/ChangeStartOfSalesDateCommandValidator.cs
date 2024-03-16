using FluentValidation;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Commands.ChangeStartOfSalesDate;

public class ChangeStartOfSalesDateCommandValidator : AbstractValidator<ChangeStartOfSalesDateCommand>
{
    public ChangeStartOfSalesDateCommandValidator() 
    {
        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("EventId is required.");
        
        RuleFor(x => x.StartOfSalesDate)
            .NotEmpty()
            .WithMessage("Start of sales is required.");
        
        RuleFor(x => x.StartOfSalesDate)
            .GreaterThan(SystemClock.Now)
            .WithMessage("Start of sales must be in the future.");
    }
}