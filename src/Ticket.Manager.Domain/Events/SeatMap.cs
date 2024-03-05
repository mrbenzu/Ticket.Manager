using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events;

public record SeatMap(int NoNumericPlaceCount, int RowsCount, int SeatsInRowCount) : ValueObject;