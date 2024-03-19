using FluentAssertions;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Seats;
using Ticket.Manager.Domain.Seats.Events;
using Ticket.Manager.Domain.UnitTests.Common;
using Xunit;

namespace Ticket.Manager.Domain.UnitTests.Seats;

public class SeatTests : TestBase
{
    private readonly Guid _eventId = Guid.NewGuid();
    private const bool IsUnnumberedSeat = false;
    private const int Sector = 1;
    private const int RowNumber = 1;
    private const int SeatNumber = 1;
    
    [Fact]
    public void Seat_Create_Success()
    {
        var seat = CreateSeat();
        
        seat.Should().NotBeNull();
        seat.EventId.Should().Be(_eventId);
        seat.SeatDetails.IsUnnumberedSeat.Should().Be(IsUnnumberedSeat);
        seat.SeatDetails.Sector.Should().Be(Sector);
        seat.SeatDetails.RowNumber.Should().Be(RowNumber);
        seat.SeatDetails.SeatNumber.Should().Be(SeatNumber);
    }

    private Seat CreateSeat() => Seat.Create(_eventId, IsUnnumberedSeat, Sector, RowNumber, SeatNumber);

    [Fact]
    public void Seat_Reserve_Success()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        SystemClock.Set(new DateTime(2024, 03, 20, 20, 00, 00));
        var expectedReservedTo = SystemClock.Now.AddMinutes(15);

        seat.Reserve(userId, []);

        seat.IsReserved.Should().BeTrue();
        seat.ReservedTo.Should().Be(expectedReservedTo);
        seat.UserId.Should().Be(userId);
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().NotBeNull();
        seatReservedEvent?.SeatId.Should().Be(seat.Id);
    }
}