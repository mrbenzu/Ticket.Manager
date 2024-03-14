using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Orders;

public class Order : Entity, IAggregateRoot
{
    public Guid Id { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public Guid EventId { get; private set; }

    public IReadOnlyCollection<Guid> Seats => _seats.AsReadOnly();

    private readonly List<Guid> _seats;
    
    public bool IsPaid { get; private set; }

    private Order(Guid id, Guid userId, Guid eventId, List<Guid> seats)
    {
        Id = id;
        UserId = userId;
        EventId = eventId;
        IsPaid = false;
        _seats = seats.ToList();
    }

    public static Result<Order> Create(Guid userId, Guid eventId, List<Guid> seats)
    {
        var id = Guid.NewGuid();
        var order = new Order(id, userId, eventId, seats);

        return Result.Success(order);
    }
}