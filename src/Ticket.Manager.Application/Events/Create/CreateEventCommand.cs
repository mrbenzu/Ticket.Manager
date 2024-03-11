using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Events.Create;

public record CreateEventCommand(string Name, DateTime StartDate, DateTime StartOfSalesDate, Guid PlaceId) : ICommand<Result>;