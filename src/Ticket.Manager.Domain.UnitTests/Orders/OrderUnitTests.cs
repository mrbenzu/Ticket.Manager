using FluentAssertions;
using Ticket.Manager.Domain.Orders;
using Ticket.Manager.Domain.Orders.BusinessRules;
using Ticket.Manager.Domain.Orders.Events;
using Ticket.Manager.Domain.UnitTests.Common;
using Xunit;

namespace Ticket.Manager.Domain.UnitTests.Orders;

public class OrderUnitTests : TestBase
{
    private readonly Guid _userId = Guid.NewGuid();
    private readonly Guid _eventId = Guid.NewGuid();
    private readonly Guid _seat1Id = Guid.NewGuid();
    private const decimal Seat1Price = 123.2M;
    private readonly Guid _seat2Id = Guid.NewGuid();
    private const decimal Seat2Price = 11.2M;
    private readonly List<Seat> _seats;
    
    public OrderUnitTests()
    {
        var seat1 = Seat.Create(_seat1Id, Seat1Price);
        var seat2 = Seat.Create(_seat2Id, Seat2Price);
        _seats = new List<Seat> { seat1, seat2 };
    }
    
    [Fact]
    public void Order_Create_Success()
    {
        var order = CreateOrder();
        
        order.UserId.Should().Be(_userId);
        order.EventId.Should().Be(_eventId);
        order.Seats.Should().BeEquivalentTo(_seats);
        order.IsPaid.Should().BeFalse();
        order.IsCancel.Should().BeFalse();
        
        var orderCreatedEvent = GetDomainEvent<OrderCreatedEvent>(order);
        orderCreatedEvent.Should().NotBeNull();
        orderCreatedEvent?.UserId.Should().Be(order.UserId);
        orderCreatedEvent?.SeatIds.Should().BeEquivalentTo(_seats.Select(x => x.SeatId));
    }
    
    [Fact]
    public void Order_Return_Success()
    {
        var order = CreateOrder();
        
        order.Return(_userId);

        foreach (var seat in order.Seats)
        {
            seat.IsReturned.Should().BeTrue();
        }
        
        order.UserId.Should().Be(_userId);
        order.EventId.Should().Be(_eventId);
        order.Seats.Should().BeEquivalentTo(_seats);
        order.IsPaid.Should().BeFalse();
        order.IsCancel.Should().BeFalse();
        
        var orderReturnedEvent = GetDomainEvent<OrderReturnedEvent>(order);
        orderReturnedEvent.Should().NotBeNull();
        orderReturnedEvent?.OrderId.Should().Be(order.Id);
        orderReturnedEvent?.UserId.Should().Be(order.UserId);
        orderReturnedEvent?.SeatIds.Should().BeEquivalentTo(_seats.Select(x => x.SeatId));
    }
    
    [Fact]
    public void Order_Return_ItIsNotUserOrderRule_RuleIsBroken()
    {
        var order = CreateOrder();
        var user2Id = Guid.NewGuid();
        
        AssertBrokenRule<ItIsNotUserOrderRule>(() => order.Return(user2Id));
        
        var orderReturnedEvent = GetDomainEvent<OrderReturnedEvent>(order);
        orderReturnedEvent.Should().BeNull();
    }
    
    [Fact]
    public void Order_Approve_Success()
    {
        var order = CreateOrder();
        
        order.Approve();
        order.IsPaid.Should().BeTrue();
        
        var orderApprovedEvent = GetDomainEvent<OrderApprovedEvent>(order);
        orderApprovedEvent.Should().NotBeNull();
        orderApprovedEvent?.OrderId.Should().Be(order.Id);
        orderApprovedEvent?.UserId.Should().Be(order.UserId);
        orderApprovedEvent?.SeatIds.Should().BeEquivalentTo(_seats.Select(x => x.SeatId));
    }
    
    [Fact]
    public void Order_Cancel_Success()
    {
        var order = CreateOrder();
        
        order.Cancel();
        order.IsCancel.Should().BeTrue();
        
        var orderCanceledEvent = GetDomainEvent<OrderCanceledEvent>(order);
        orderCanceledEvent.Should().NotBeNull();
        orderCanceledEvent?.OrderId.Should().Be(order.Id);
        orderCanceledEvent?.UserId.Should().Be(order.UserId);
        orderCanceledEvent?.SeatIds.Should().BeEquivalentTo(_seats.Select(x => x.SeatId));
    }

    private Order CreateOrder() => Order.Create(_userId, _eventId, _seats);
}