using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Cancel;

public record CancelEventCommand(Guid EventId) : ICommand<Result>;