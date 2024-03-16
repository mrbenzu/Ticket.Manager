using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Events.Commands.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Events;
using Ticket.Manager.Domain.Places;

namespace Ticket.Manager.Application.Events.Commands.Create;

public class CreateEventCommandHandler(IEventRepository eventRepository, IPlaceRepository placeRepository) : ICommandHandler<CreateEventCommand, Result>
{
    public async Task<Result> Handle(CreateEventCommand command, CancellationToken cancellationToken)
    {
        var place = await placeRepository.Get(command.PlaceId, cancellationToken);
        if (place is null)
        {
            return Result.Failure(EventApplicationErrors.PlaceDoesntExist);
        }
        
        var result = Event.Create(command.Name, command.StartDate, command.StartOfSalesDate, command.PlaceId, 
            place.UnnumberedSeatsMap.SectorCount, place.UnnumberedSeatsMap.SeatsInSectorCount,
            place.SeatsMap.SectorCount, place.SeatsMap.RowsCount, place.SeatsMap.SeatsInRowCount);
        
        if (result is not { IsSuccess: true, Value: not null }) 
            return Result.Failure(result.Error);

        await eventRepository.Add(result.Value, cancellationToken);
        
        return Result.Success();
    }
}