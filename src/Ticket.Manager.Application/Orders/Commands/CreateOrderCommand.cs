using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Orders.Commands;

public record CreateOrderCommand(Guid EventId, Guid UserId, IReadOnlyCollection<Seat> Seats) : ICommand<Result>;

public record Seat(Guid SeatId, decimal Price);