using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Commands.ChangeName;

public record ChangeNameCommand(Guid EventId, string Name) : ICommand;