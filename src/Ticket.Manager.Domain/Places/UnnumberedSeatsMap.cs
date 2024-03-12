using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Places;

public class UnnumberedSeatsMap : Entity
{
    public Guid Id { get; private set; }
    
    public int SectorCount { get; private set; }
    
    public int SeatsInSectorCount { get; private set; }

    private UnnumberedSeatsMap(Guid id, int sectorCount, int seatsInSectorCount)
    {
        Id = id;
        SectorCount = sectorCount;
        SeatsInSectorCount = seatsInSectorCount;
    }

    public static UnnumberedSeatsMap Create(int sectorCount, int seatsInSectorCount)
    {
        var id = Guid.NewGuid();
        return new UnnumberedSeatsMap(id, sectorCount, seatsInSectorCount);
    }
}
