using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.BusinessRules;

public class SeatHasToBeReservedByUserRule(Guid userId, Guid userId2) : IBusinessRule
{
    public bool IsBroken() => userId != userId2;

    public string Message => "Seat is not reserved by user.";
}