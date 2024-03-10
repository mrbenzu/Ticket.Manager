using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Seats.Errors;

public static class SeatApplicationErrors
{
    public static readonly Error SeatDoesntExist = new Error("SeatDoesntExist", "Seat doesn't exist.");
}