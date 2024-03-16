using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Events.Commands.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Commands.Reopen;

public class ReopenEventCommandHandler(IEventRepository eventRepository) : ICommandHandler<ReopenEventCommand, Result>
{
    public async Task<Result> Handle(ReopenEventCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);
        if (@event is null)
        {
            return Result.Failure(EventApplicationErrors.EventDoesntExist);
        }

        var result = @event.Reopen();

        return result.IsFailure 
            ? Result.Failure(result.Error) 
            : Result.Success();
    }
}