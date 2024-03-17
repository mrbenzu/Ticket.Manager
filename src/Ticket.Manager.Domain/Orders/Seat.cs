using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Orders;

public class Seat : Entity
{
    public Guid SeatId { get; private set; }
    
    public bool IsReturned { get; private set; }
    
    public decimal Price { get; private set; }

    private Seat(Guid seatId, decimal price)
    {
        SeatId = seatId;
        Price = price;
        IsReturned = false;
    }

    public static Seat Create(Guid seatId, decimal price)
    {
        var seat = new Seat(seatId, price);

        return new Seat(seatId, price);
    }

    public void Return()
    {
        IsReturned = true;
    }
}