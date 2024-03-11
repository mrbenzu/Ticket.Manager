using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Seats.Commands.WithdrawnSeat;

public record WithdrawnSeatCommand(Guid SeatId) : ICommand<Result>;