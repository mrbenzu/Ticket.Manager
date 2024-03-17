using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats.BusinessRules;

public class CannotLeaveEmptySeatNearReservedRule(SeatDetails seatDetails, IReadOnlyCollection<int> reservedSeatNumbersInRow) : IBusinessRule
{
    public bool IsBroken() => !seatDetails.IsUnnumberedSeat &&
                              (reservedSeatNumbersInRow.Any(x => x == seatDetails.SeatNumber + 2)  ||
                               reservedSeatNumbersInRow.Any(x => x == seatDetails.SeatNumber - 2));

    public string Message => "Cannot leave empty seat near already reserved.";
}