using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events;

public record SeatsMap(int SectorCount, int RowsCount, int SeatsInRowCount) : ValueObject;