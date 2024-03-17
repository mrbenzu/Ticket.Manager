using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Commands.ChangeName;

public class ChangeNameCommandHandler(IEventRepository eventRepository) : ICommandHandler<ChangeNameCommand>
{
    public async Task Handle(ChangeNameCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);
        
        @event.ChangeName(command.Name);
    }
}