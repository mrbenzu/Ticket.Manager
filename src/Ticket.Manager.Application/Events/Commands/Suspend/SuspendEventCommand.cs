using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Commands.Suspend;

public record SuspendEventCommand(Guid EventId) : ICommand;