using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Reopen;

public record ReopenEventCommand(Guid EventId) : ICommand<Result>;