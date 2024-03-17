using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.BusinessRules;

public class SeatCannotBeSuspendedRule(bool isSuspended) : IBusinessRule
{
    public bool IsBroken() => isSuspended;

    public string Message => "Seat is suspended.";
}