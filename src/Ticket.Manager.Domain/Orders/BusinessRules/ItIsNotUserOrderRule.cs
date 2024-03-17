using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Orders.BusinessRules;

public class ItIsNotUserOrderRule(Guid userId, Guid user2) : IBusinessRule
{
    public bool IsBroken() => userId != user2;

    public string Message => "It's not user order.";
}