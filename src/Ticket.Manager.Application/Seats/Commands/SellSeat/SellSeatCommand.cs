using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Seats.Commands.SellSeat;

public record SellSeatCommand(Guid SeatId, Guid UserId) : ICommand;