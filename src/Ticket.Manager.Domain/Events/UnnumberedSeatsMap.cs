using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events;

public record UnnumberedSeatsMap(int SectorCount, int SeatsInSectorCount) : ValueObject;