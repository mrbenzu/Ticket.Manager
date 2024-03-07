using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Events.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Cancel;

public class CancelEventCommandHandler(IEventRepository eventRepository) : ICommandHandler<CancelEventCommand, Result>
{
    public async Task<Result> Handle(CancelEventCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);
        if (@event is null)
        {
            return Result.Failure(EventApplicationErrors.EventDoesntExist);
        }

        var result = @event.Cancel();

        return result.IsFailure 
            ? Result.Failure(result.Error) 
            : Result.Success();
    }
}