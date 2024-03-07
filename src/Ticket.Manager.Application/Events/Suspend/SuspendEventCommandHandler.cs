using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Events.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Suspend;

public class SuspendEventCommandHandler(IEventRepository eventRepository) : ICommandHandler<SuspendEventCommand, Result>
{
    public async Task<Result> Handle(SuspendEventCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);
        if (@event is null)
        {
            return Result.Failure(EventApplicationErrors.EventDoesntExist);
        }

        var result = @event.Suspend();

        return result.IsFailure 
            ? Result.Failure(result.Error) 
            : Result.Success();
    }
}