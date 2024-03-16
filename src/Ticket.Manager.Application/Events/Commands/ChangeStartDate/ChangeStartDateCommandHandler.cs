using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Events.Commands.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Commands.ChangeStartDate;

public class ChangeStartDateCommandHandler(IEventRepository eventRepository) : ICommandHandler<ChangeStartDateCommand, Result>
{
    public async Task<Result> Handle(ChangeStartDateCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);
        if (@event is null)
        {
            return Result.Failure(EventApplicationErrors.EventDoesntExist);
        }

        var result = @event.ChangeStartDate(command.StartDate);

        return result;
    }
}