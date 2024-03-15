using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Domain.Orders;

public static class OrderErrors
{
    public static readonly Error IsNotUserOrder = new Error("IsNotUserOrder", "It's not user order.");
}