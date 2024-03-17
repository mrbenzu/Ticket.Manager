using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.BusinessRules;

public class SeatCannotBeSoldRule(bool isSold) : IBusinessRule
{
    public bool IsBroken() => isSold;

    public string Message => "Seat is already sold.";
}