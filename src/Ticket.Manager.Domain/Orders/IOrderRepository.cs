﻿namespace Ticket.Manager.Domain.Orders;

public interface IOrderRepository
{
    Task<Seat?> Get(Guid id, CancellationToken cancellationToken);
}