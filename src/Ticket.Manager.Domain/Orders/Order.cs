using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Common.Domain;
using Ticket.Manager.Domain.Orders.Events;

namespace Ticket.Manager.Domain.Orders;

public class Order : Entity, IAggregateRoot
{
    public Guid Id { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public Guid EventId { get; private set; }

    public IReadOnlyCollection<Seat> Seats => _seats.AsReadOnly();

    private readonly List<Seat> _seats;
    
    public bool IsPaid { get; private set; }

    private Order(Guid id, Guid userId, Guid eventId, List<Seat> seats)
    {
        Id = id;
        UserId = userId;
        EventId = eventId;
        IsPaid = false;
        _seats = seats.ToList();
    }

    public static Result<Order> Create(Guid userId, Guid eventId, List<Seat> seats)
    {
        var id = Guid.NewGuid();
        var order = new Order(id, userId, eventId, seats);

        order.AddDomainEvent(new OrderCreatedEvent(id, userId, seats.Select(x => x.SeatId)));

        return Result.Success(order);
    }

    public Result Return(Guid userId)
    {
        if (UserId != userId)
        {
            return Result.Failure(OrderErrors.IsNotUserOrder);
        }
        
        _seats.ForEach(x => x.Return());
        
        AddDomainEvent(new OrderReturnedEvent(Id, _seats.Select(x => x.SeatId)));

        return Result.Success();
    }

    public Result Approve(Guid userId)
    {
        if (UserId != userId)
        {
            return Result.Failure(OrderErrors.IsNotUserOrder);
        }

        IsPaid = true;
        
        AddDomainEvent(new OrderApprovedEvent(Id, UserId, _seats.Select(x => x.SeatId)));

        return Result.Success();
    }
    
    public Result Cancel(Guid userId)
    {
        if (UserId != userId)
        {
            return Result.Failure(OrderErrors.IsNotUserOrder);
        }

        IsPaid = true;
        
        AddDomainEvent(new OrderCanceledEvent(Id, userId, _seats.Select(x => x.SeatId)));

        return Result.Success();
    }
}