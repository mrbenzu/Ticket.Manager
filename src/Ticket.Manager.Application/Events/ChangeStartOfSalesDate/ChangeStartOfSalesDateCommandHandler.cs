using Ticket.Manager.Application.Common;
using Ticket.Manager.Application.Events.ChangeStartDate;
using Ticket.Manager.Application.Events.Errors;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Application.Events.ChangeStartOfSalesDate;

public class ChangeStartOfSalesDateCommandHandler(IEventRepository eventRepository) : ICommandHandler<ChangeStartOfSalesDateCommand, Result>
{
    public async Task<Result> Handle(ChangeStartOfSalesDateCommand command, CancellationToken cancellationToken)
    {
        var @event = await eventRepository.Get(command.EventId, cancellationToken);
        if (@event is null)
        {
            return Result.Failure(EventApplicationErrors.EventDoesntExist);
        }

        var result = @event.ChangeStartOfSalesDate(command.StartOfSalesDate);

        return result;
    }
}