using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Suspend;

public record SuspendEventCommand(Guid EventId) : ICommand<Result>;