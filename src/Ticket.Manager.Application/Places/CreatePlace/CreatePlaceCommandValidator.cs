using FluentValidation;

namespace Ticket.Manager.Application.Places.CreatePlace;

public class CreatePlaceCommandValidator  : AbstractValidator<CreatePlaceCommand>
{
    public CreatePlaceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Number is required.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.UnnumberedSeatsSectorCount)
            .GreaterThanOrEqualTo(0).WithMessage("Number of unnumbered seats sector count should not be negative");
                
        RuleFor(x => x.UnnumberedSeatsInSectorCount)
            .GreaterThanOrEqualTo(0).WithMessage("Number of unnumbered seats in sector count should not be negative");

        RuleFor(x => x.SectorCount)
            .GreaterThanOrEqualTo(0).WithMessage("Number of sectors should not be negative");

        RuleFor(x => x.RowsCount)
            .GreaterThanOrEqualTo(0).WithMessage("Number of rows should not be negative");

        RuleFor(x => x.SeatsInRowCount)
            .GreaterThanOrEqualTo(0).WithMessage("Number of seats In Row should not be negative");
    }
}