using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.Commands.ChangeStartOfSalesDate;

public class ChangeStartOfSalesDateCommandHandler(IEventRepository eventRepository) : ICommandHandler<ChangeStartOfSalesDateCommand>
{
    public async Task Handle(ChangeStartOfSalesDateCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);
        
        @event.ChangeStartOfSalesDate(command.StartOfSalesDate);
    }
}