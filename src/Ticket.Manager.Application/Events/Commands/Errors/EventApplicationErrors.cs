using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Commands.Errors;

public static class EventApplicationErrors
{
    public static readonly Error EventDoesntExist = new Error("EventDoesntExist", "Event doesn't exist.");
    public static readonly Error PlaceDoesntExist = new Error("PlaceDoesntExist", "Place doesn't exist.");
}