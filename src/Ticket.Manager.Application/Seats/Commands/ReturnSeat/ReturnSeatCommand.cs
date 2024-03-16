using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Seats.Commands.ReturnSeat;

public record ReturnSeatCommand(Guid SeatId, Guid UserId) : ICommand<Result>;