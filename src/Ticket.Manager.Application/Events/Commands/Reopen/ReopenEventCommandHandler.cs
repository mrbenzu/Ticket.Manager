using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Commands.Reopen;

public class ReopenEventCommandHandler(IEventRepository eventRepository) : ICommandHandler<ReopenEventCommand>
{
    public async Task Handle(ReopenEventCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);

        @event.Reopen();
    }
}