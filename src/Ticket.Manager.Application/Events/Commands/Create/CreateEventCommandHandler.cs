using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events;
using Ticket.Manager.Domain.Places;

namespace Ticket.Manager.Application.Events.Commands.Create;

public class CreateEventCommandHandler(IEventRepository eventRepository, IPlaceRepository placeRepository) : ICommandHandler<CreateEventCommand>
{
    public async Task Handle(CreateEventCommand command, CancellationToken cancellationToken)
    {
        var place = await placeRepository.Get(command.PlaceId, cancellationToken);
        
        var @event = Event.Create(command.Name, command.StartDate, command.StartOfSalesDate, command.PlaceId, 
            place.UnnumberedSeatsMap.SectorCount, place.UnnumberedSeatsMap.SeatsInSectorCount,
            place.SeatsMap.SectorCount, place.SeatsMap.RowsCount, place.SeatsMap.SeatsInRowCount);
        
        await eventRepository.Add(@event, cancellationToken);
    }
}