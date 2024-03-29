﻿using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid EventId, Guid UserId, IReadOnlyCollection<Seat> Seats) : ICommand;

public record Seat(Guid SeatId, decimal Price);