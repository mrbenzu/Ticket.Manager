using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Seats.Commands.ExtendReservationTime;

public record ExtendReservationTimeCommand(Guid SeatId, Guid UserId) : ICommand;