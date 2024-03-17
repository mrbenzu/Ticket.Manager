using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events.BusinessRules;

public class EventCannotReopenCanceledEventRule(bool isCanceled) : IBusinessRule
{
    public bool IsBroken() => isCanceled;

    public string Message => "Cannot reopen canceled event.";
}