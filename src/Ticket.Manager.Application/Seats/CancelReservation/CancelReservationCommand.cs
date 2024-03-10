using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Seats.CancelReservation;

public record CancelReservationCommand(Guid SeatId) : ICommand<Result>;