using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Commands.ChangeStartOfSalesDate;

public record ChangeStartOfSalesDateCommand(Guid EventId, DateTime StartOfSalesDate) : ICommand;