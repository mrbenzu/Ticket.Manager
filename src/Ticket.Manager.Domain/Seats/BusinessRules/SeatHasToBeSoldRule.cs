using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.BusinessRules;

public class SeatHasToBeSold(bool isSold) : IBusinessRule
{
    public bool IsBroken() => !isSold;

    public string Message => "Seat is not sold.";
}