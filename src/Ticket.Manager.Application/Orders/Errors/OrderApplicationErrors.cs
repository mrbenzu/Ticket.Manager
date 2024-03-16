using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Orders.Errors;

public static class OrderApplicationErrors
{
    public static readonly Error OrderDoesntExist = new Error("OrderDoesntExist", "Order doesn't exist.");
}