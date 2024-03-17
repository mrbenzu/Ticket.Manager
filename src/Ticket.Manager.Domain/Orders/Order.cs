using Ticket.Manager.Domain.Common.Domain;
using Ticket.Manager.Domain.Orders.BusinessRules;
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
    
    public bool IsCancel { get; private set; }

    private Order(Guid id, Guid userId, Guid eventId, List<Seat> seats)
    {
        Id = id;
        UserId = userId;
        EventId = eventId;
        IsPaid = false;
        IsCancel = false;
        _seats = seats.ToList();

        AddDomainEvent(new OrderCreatedEvent(id, userId, seats.Select(x => x.SeatId)));
    }

    public static Order Create(Guid userId, Guid eventId, List<Seat> seats)
    {
        var id = Guid.NewGuid();
        var order = new Order(id, userId, eventId, seats);
        return order;
    }

    public void Return(Guid userId)
    {
        CheckRule(new ItIsNotUserOrderRule(UserId, userId));
        
        _seats.ForEach(x => x.Return());
        
        AddDomainEvent(new OrderReturnedEvent(Id, userId, _seats.Select(x => x.SeatId)));
    }

    public void Approve()
    {
        IsPaid = true;
        
        AddDomainEvent(new OrderApprovedEvent(Id, UserId, _seats.Select(x => x.SeatId)));
    }
    
    public void Cancel()
    {
        IsCancel = true;
        
        AddDomainEvent(new OrderCanceledEvent(Id, UserId, _seats.Select(x => x.SeatId)));
    }
}