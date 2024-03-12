using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Places;

public class SeatsMap : Entity
{
    public Guid Id { get; private set; }
    
    public int SectorCount { get; private set; }
    
    public int RowsCount { get; private set; }
    
    public int SeatsInRowCount { get; private set; }

    private SeatsMap(Guid id, int sectorCount, int rowsCount, int seatsInRowCount)
    {
        Id = id;
        SectorCount = sectorCount;
        RowsCount = rowsCount;
        SeatsInRowCount = seatsInRowCount;  
    }

    public static SeatsMap Create(int sectorCount, int rowsCount, int seatsInRowCount)
    {
        var id = Guid.NewGuid();
        return new SeatsMap(id, sectorCount, rowsCount, seatsInRowCount);
    }
}