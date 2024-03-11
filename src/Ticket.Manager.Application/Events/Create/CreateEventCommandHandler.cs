using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Events.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Events;
using Ticket.Manager.Domain.Places;
using SeatMap = Ticket.Manager.Domain.Events.SeatMap;

namespace Ticket.Manager.Application.Events.Create;

public class CreateEventCommandHandler(IEventRepository eventRepository, IPlaceRepository placeRepository) : ICommandHandler<CreateEventCommand, Result>
{
    public async Task<Result> Handle(CreateEventCommand command, CancellationToken cancellationToken)
    {
        var place = await placeRepository.Get(command.PlaceId, cancellationToken);
        if (place is null)
        {
            return Result.Failure(EventApplicationErrors.PlaceDoesntExist);
        }

        var seatMap = new SeatMap(place.SeatMap.NoNumericPlaceCount,
            place.SeatMap.SectorCount,
            place.SeatMap.RowsCount,
            place.SeatMap.SeatsInRowCount);
        
        var result = Event.Create(command.Name, command.StartDate, command.StartOfSalesDate, command.PlaceId, seatMap);
        if (result is not { IsSuccess: true, Value: not null }) 
            return Result.Failure(result.Error);

        await eventRepository.Add(result.Value, cancellationToken);
        
        return Result.Success();
    }
}