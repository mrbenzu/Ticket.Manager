using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Places;

public class Place : IAggregateRoot
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; }
    
    public Address Address { get; private set; }

    public UnnumberedSeatsMap UnnumberedSeatsMap { get; private set; }
    
    public SeatsMap SeatsMap { get; private set; }

    // EF
    private Place()
    {
        
    }

    private Place(Guid id, string name, Address address, UnnumberedSeatsMap unnumberedSeatsMap, SeatsMap seatsMap)
    {
        Id = id;
        Name = name;
        Address = address;
        UnnumberedSeatsMap = unnumberedSeatsMap;
        SeatsMap = seatsMap;
    }

    public static Place Create(string name, 
        string street, string number, string city, 
        int unnumberedSeatsSectorCount, int unnumberedSeatsInSectorCount,
        int sectorCount, int rowsCount, int seatsInRowCount)
    {
        var id = Guid.NewGuid();
        var address = new Address(street, number, city);
        var unnumberedSeatsMap = UnnumberedSeatsMap.Create(unnumberedSeatsSectorCount, unnumberedSeatsInSectorCount);
        var seatsMap = SeatsMap.Create(sectorCount, rowsCount, seatsInRowCount);
        
        var place = new Place(id, name, address, unnumberedSeatsMap, seatsMap);

        return place;
    }
}