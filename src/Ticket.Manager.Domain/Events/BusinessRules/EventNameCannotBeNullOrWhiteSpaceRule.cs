using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events.BusinessRules;

public class EventNameCannotBeNullOrWhiteSpaceRule(string name) : IBusinessRule
{
    public bool IsBroken() => string.IsNullOrEmpty(name);

    public string Message => "Event is cancelled.";
}