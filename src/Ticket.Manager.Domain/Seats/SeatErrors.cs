﻿using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Domain.Seats;

public static class SeatErrors
{
    public static readonly Error IsReserved = new Error("IsReserved", "Seat is reserved.");
    public static readonly Error IsAlreadyReservedByOwner = new Error("IsAlreadyReservedByOwner", "Seat is already reserved by owner.");
    public static readonly Error IsAlreadySold = new Error("IsSold", "Seat is already sold.");
    public static readonly Error IsSuspended = new Error("IsSuspended", "Seat is suspended.");
    public static readonly Error IsNotSuspended = new Error("IsSuspended", "Seat is not suspended.");
    public static readonly Error IsWithdrawn = new Error("IsWithdrawn", "Seat is withdrawn.");
    public static readonly Error IsNotSold = new Error("IsNotSold", "Seat is not sold.");
    public static readonly Error IsNotReservedByOwner = new Error("IsNotReservedByOwner", "Seat is not reserved by owner.");
    public static readonly Error ReservationExpired = new Error("ReservationExpired", "Reservation time has expired.");
    public static readonly Error CannotLeaveEmptySeatNearReserved = new Error("CannotLeaveEmptySeatNearReserved", "Cannot leave empty seat near already reserved.");

}