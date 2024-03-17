using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.BusinessRules;

public class CannotReopenNotSuspendedSeatRule(bool isSuspended) : IBusinessRule
{
    public bool IsBroken() => !isSuspended;

    public string Message => "Cannot reopen not suspended seat.";
}