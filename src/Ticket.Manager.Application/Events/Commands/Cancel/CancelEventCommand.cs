using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Commands.Cancel;

public record CancelEventCommand(Guid EventId) : ICommand;