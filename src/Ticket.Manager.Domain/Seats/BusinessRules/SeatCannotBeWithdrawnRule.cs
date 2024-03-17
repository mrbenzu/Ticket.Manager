using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.BusinessRules;

public class SeatCannotBeWithdrawnRule(bool isWithdrawn) : IBusinessRule
{
    public bool IsBroken() => isWithdrawn;

    public string Message => "Seat is withdrawn.";
}