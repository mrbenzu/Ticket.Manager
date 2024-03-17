using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Commands.Suspend;

public class SuspendEventCommandHandler(IEventRepository eventRepository) : ICommandHandler<SuspendEventCommand>
{
    public async Task Handle(SuspendEventCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken); 
        
        @event.Suspend();
    }
}