using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Commands.Cancel;

public class CancelEventCommandHandler(IEventRepository eventRepository) : ICommandHandler<CancelEventCommand>
{
    public async Task Handle(CancelEventCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);

        @event.Cancel();
    }
}