using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Places;

public class SeatMap : Entity
{
    public long Id { get; private set; }
    public int NoNumericPlaceCount { get; private set; }
    public int RowsCount { get; private set; }
    public int SeatsInRowCount { get; private set; }

    private SeatMap(int noNumericPlaceCount, int rowsCount, int seatsInRowCount)
    {
        NoNumericPlaceCount = noNumericPlaceCount;
        RowsCount = rowsCount;
        SeatsInRowCount = seatsInRowCount;  
    }

    public static SeatMap Create(int noNumericPlaceCount, int rowsCount, int seatsInRowCount)
    {
        return new SeatMap(noNumericPlaceCount, rowsCount, seatsInRowCount);
    }
}