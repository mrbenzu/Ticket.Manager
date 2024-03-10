using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats;

public record SeatDetails(bool IsNonNumeric, int Sector, int RowNumber, int SeatNumber) : ValueObject;