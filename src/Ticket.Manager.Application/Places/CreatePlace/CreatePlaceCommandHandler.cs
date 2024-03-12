using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Places;

namespace Ticket.Manager.Application.Places.CreatePlace;

public class CreatePlaceCommandHandler(IPlaceRepository placeRepository) : ICommandHandler<CreatePlaceCommand, Result>
{
    public async Task<Result> Handle(CreatePlaceCommand command, CancellationToken cancellationToken)
    {
        var result = Place.Create(command.Name, 
            command.Street, command.Number, command.City, 
            command.UnnumberedSeatsSectorCount, command.UnnumberedSeatsInSectorCount,
            command.SectorCount, command.RowsCount, command.SeatsInRowCount);
        if (result is not { IsSuccess: true, Value: not null }) 
            return Result.Failure(result.Error);

        await placeRepository.Add(result.Value, cancellationToken);
        
        return Result.Success();
    }
}