﻿using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.ChangeStartDate;

public record ChangeStartDateCommand(Guid EventId, DateTime StartDate) : ICommand<Result>;