using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Seats.Commands.CancelReservation;

public record CancelReservationCommand(Guid SeatId, Guid UserId) : ICommand;