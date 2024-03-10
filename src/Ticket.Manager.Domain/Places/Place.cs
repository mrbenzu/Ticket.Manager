using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Places;

public class Place : IAggregateRoot
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; }
    
    public Address Address { get; private set; }

    public SeatMap SeatMap { get; private set; }

    // EF
    private Place()
    {
        
    }

    private Place(Guid id, string name, Address address, SeatMap seatMap)
    {
        Id = id;
        Name = name;
        Address = address;
        SeatMap = seatMap;
    }

    public static Result<Place> Create(string name, 
        string street, string number, string city, 
        int noNumericPlaceCount, int rowsCount, int seatsInRowCount)
    {
        var id = Guid.NewGuid();
        
        var address = new Address(street, number, city);
        var seatsMap = SeatMap.Create(noNumericPlaceCount, rowsCount, seatsInRowCount);
        
        var place = new Place(id, name, address, seatsMap);

        return Result.Success(place);
    }
}