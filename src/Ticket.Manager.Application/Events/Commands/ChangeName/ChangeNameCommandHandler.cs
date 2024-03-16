using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Events.Commands.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Commands.ChangeName;

public class ChangeNameCommandHandler(IEventRepository eventRepository) : ICommandHandler<ChangeNameCommand, Result>
{
    public async Task<Result> Handle(ChangeNameCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);
        if (@event is null)
        {
            return Result.Failure(EventApplicationErrors.EventDoesntExist);
        }

        var result = @event.ChangeName(command.Name);

        return result;
    }
}