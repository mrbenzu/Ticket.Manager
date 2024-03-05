using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Domain.Events;

public static class EventErrors
{
    public static readonly Error IsCanceled = new Error("IsCanceled", "Event is cancelled.");
}