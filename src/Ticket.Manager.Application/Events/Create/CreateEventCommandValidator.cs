using FluentValidation;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Create;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Event name is required.");
        
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage("Maximum length for event name is 100 char.");
        
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required..");
        
        RuleFor(x => x.StartDate)
            .GreaterThan(SystemClock.Now)
            .WithMessage("Start date must be in the future.");
        
        RuleFor(x => x.StartOfSalesDate)
            .NotEmpty()
            .WithMessage("Start of sales is required.");
        
        RuleFor(x => x.StartOfSalesDate)
            .GreaterThan(SystemClock.Now)
            .WithMessage("Start of sales date must be in the future.");
        
        RuleFor(x => x.StartOfSalesDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("Start of sales date must be after start date.");
        
        RuleFor(x => x.PlaceId)
            .NotEmpty()
            .WithMessage("PlaceId is required.");
    }
}