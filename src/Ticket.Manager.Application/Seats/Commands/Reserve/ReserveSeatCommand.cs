﻿using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Seats.Commands.Reserve;

public record ReserveSeatCommand(Guid SeatId, Guid UserId) : ICommand;