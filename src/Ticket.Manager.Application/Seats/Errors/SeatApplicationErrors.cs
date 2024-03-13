using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Seats.Errors;

public static class SeatApplicationErrors
{
    public static readonly Error SeatDoesntExist = new Error("SeatDoesntExist", "Seat doesn't exist.");
    public static readonly Error EventDoesntContainSeats = new Error("EventDoesntContainSeats", "Event doesn't contain seats.");
}