using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Orders.Commands.ReturnOrder;

public record ReturnOrderCommand(Guid OrderId, Guid UserId) : ICommand;