using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Commands.ChangeStartDate;

public class ChangeStartDateCommandHandler(IEventRepository eventRepository) : ICommandHandler<ChangeStartDateCommand>
{
    public async Task Handle(ChangeStartDateCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);

        @event.ChangeStartDate(command.StartDate);
    }
}