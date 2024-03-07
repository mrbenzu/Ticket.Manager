using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Domain.Events;

public static class EventErrors
{
    public static readonly Error IsCanceled = new Error("IsCanceled", "Event is cancelled.");
    public static readonly Error InvalidName = new Error("InvalidName", "Name cannot be empty.");
    public static readonly Error StartOfSalesDateIsEarlierThanStartDate = new Error("InvalidName", "Start of sales date cannot be earlier than start date.");
}