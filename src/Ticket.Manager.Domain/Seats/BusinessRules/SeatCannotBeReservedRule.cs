using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.BusinessRules;

public class SeatCannotBeReservedRule(bool isReserved, DateTime reservedTo) : IBusinessRule
{
    public bool IsBroken() => isReserved && reservedTo >= SystemClock.Now;

    public string Message => "Seat is reserved.";
}