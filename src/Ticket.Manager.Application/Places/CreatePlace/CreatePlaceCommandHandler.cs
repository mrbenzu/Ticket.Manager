using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Places;

namespace Ticket.Manager.Application.Places.CreatePlace;

public class CreatePlaceCommandHandler(IPlaceRepository placeRepository) : ICommandHandler<CreatePlaceCommand>
{
    public async Task Handle(CreatePlaceCommand command, CancellationToken cancellationToken)
    {
        var place = Place.Create(command.Name, 
            command.Street, command.Number, command.City, 
            command.UnnumberedSeatsSectorCount, command.UnnumberedSeatsInSectorCount,
            command.SectorCount, command.RowsCount, command.SeatsInRowCount);

        await placeRepository.Add(place, cancellationToken);
    }
}