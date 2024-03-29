﻿using FluentAssertions;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Seats;
using Ticket.Manager.Domain.Seats.BusinessRules;
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
    private const int SeatNumber = 4;
    
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
    
    [Fact]
    public void Seat_Reserve_Success()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();
        var expectedReservedTo = SystemClock.Now.AddMinutes(15);

        seat.Reserve(userId, []);

        seat.IsReserved.Should().BeTrue();
        seat.ReservedTo.Should().Be(expectedReservedTo);
        seat.UserId.Should().Be(userId);
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().NotBeNull();
        seatReservedEvent?.SeatId.Should().Be(seat.Id);
    }
    
    [Fact]
    public void Seat_Reserve_SeatCannotBeAlreadyReservedByUserRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();

        seat.Reserve(userId, []);
        seat.ClearDomainEvents();
        
        AssertBrokenRule<SeatCannotBeAlreadyReservedByUserRule>(() => seat.Reserve(userId, []));
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().BeNull();
    }
    
    [Fact]
    public void Seat_Reserve_SeatCannotBeReservedRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();
        var expectedReservedTo = SystemClock.Now.AddMinutes(15);

        seat.Reserve(userId, []);
        seat.ClearDomainEvents();
        
        AssertBrokenRule<SeatCannotBeReservedRule>(() => seat.Reserve(userId2, []));
        seat.IsReserved.Should().BeTrue();
        seat.ReservedTo.Should().Be(expectedReservedTo);
        seat.UserId.Should().Be(userId);
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().BeNull();
    }
    
    [Fact]
    public void Seat_Reserve_CannotLeaveEmptySeatNearReservedRule_Left_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        
        AssertBrokenRule<CannotLeaveEmptySeatNearReservedRule>(() => seat.Reserve(userId, [SeatNumber - 2]));
        seat.IsReserved.Should().BeFalse();
        seat.ReservedTo.Should().Be(DateTime.MinValue);
        seat.UserId.Should().Be(Guid.Empty);
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().BeNull();
    }
    
    [Fact]
    public void Seat_Reserve_CannotLeaveEmptySeatNearReservedRule_Right_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        
        AssertBrokenRule<CannotLeaveEmptySeatNearReservedRule>(() => seat.Reserve(userId, [SeatNumber + 2]));
        seat.IsReserved.Should().BeFalse();
        seat.ReservedTo.Should().Be(DateTime.MinValue);
        seat.UserId.Should().Be(Guid.Empty);
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().BeNull();
    }
    
    [Fact]
    public void Seat_Reserve_SeatCannotBeSoldRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();

        seat.Reserve(userId, []);
        seat.Sell(userId);
        seat.ClearDomainEvents();
        
        AssertBrokenRule<SeatCannotBeSoldRule>(() => seat.Reserve(userId, []));
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().BeNull();
    }
    
    [Fact]
    public void Seat_Reserve_SeatCannotBeWithdrawnRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();

        seat.Withdrawn();
        seat.ClearDomainEvents();
        
        AssertBrokenRule<SeatCannotBeWithdrawnRule>(() => seat.Reserve(userId, []));
        seat.IsReserved.Should().BeFalse();
        seat.ReservedTo.Should().Be(DateTime.MinValue);
        seat.UserId.Should().Be(Guid.Empty);
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().BeNull();
    }
    
    [Fact]
    public void Seat_Reserve_SeatCannotBeSuspendedRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();

        seat.Suspend();
        seat.ClearDomainEvents();
        
        AssertBrokenRule<SeatCannotBeSuspendedRule>(() => seat.Reserve(userId, []));
        seat.IsReserved.Should().BeFalse();
        seat.ReservedTo.Should().Be(DateTime.MinValue);
        seat.UserId.Should().Be(Guid.Empty);
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().BeNull();
    }
    
    [Fact]
    public void Seat_ExtendReservationTime_Success()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();

        seat.Reserve(userId, []);
        seat.ClearDomainEvents();
        
        SystemClock.Reset();
        SystemClock.Set(new DateTime(2024, 03, 20, 20, 15, 00));
        var expectedReservedTo = SystemClock.Now.AddMinutes(15);
        
        seat.ExtendReservationTime(userId);
        seat.IsReserved.Should().BeTrue();
        seat.ReservedTo.Should().Be(expectedReservedTo);
        seat.UserId.Should().Be(userId);
    }
    
    [Fact]
    public void Seat_ExtendReservationTime_SeatCannotBeSoldRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();

        seat.Reserve(userId, []);
        seat.Sell(userId);
        seat.ClearDomainEvents();
        
        AssertBrokenRule<SeatCannotBeSoldRule>(() => seat.ExtendReservationTime(userId));
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().BeNull();
    }
    
    [Fact]
    public void Seat_ExtendReservationTime_SeatHasToBeReservedByUserRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();

        seat.Reserve(userId, []);
        seat.ClearDomainEvents();
        
        AssertBrokenRule<SeatHasToBeReservedByUserRule>(() => seat.ExtendReservationTime(userId2));
        
        var seatReservedEvent = GetDomainEvent<SeatReservedEvent>(seat);
        seatReservedEvent.Should().BeNull();
    }
    
    [Fact]
    public void Seat_CancelReservation_Success()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();

        seat.Reserve(userId, []);
        seat.ClearDomainEvents();
        seat.CancelReservation(userId);
        
        seat.IsReserved.Should().BeFalse();
        seat.ReservedTo.Should().Be(default);
        seat.UserId.Should().Be(Guid.Empty);
        
        var reservationCanceledEvent = GetDomainEvent<ReservationCanceledEvent>(seat);
        reservationCanceledEvent.Should().NotBeNull();
        reservationCanceledEvent?.SeatId.Should().Be(seat.Id);
    }
    
    [Fact]
    public void Seat_CancelReservation_SeatHasToBeReservedByUserRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();
        var expectedReservedTo = SystemClock.Now.AddMinutes(15);

        seat.Reserve(userId, []);
        seat.ClearDomainEvents();
        
        AssertBrokenRule<SeatHasToBeReservedByUserRule>(() => seat.CancelReservation(userId2));

        seat.IsReserved.Should().BeTrue();
        seat.ReservedTo.Should().Be(expectedReservedTo);
        seat.UserId.Should().Be(userId);
        
        var reservationCanceledEvent = GetDomainEvent<ReservationCanceledEvent>(seat);
        reservationCanceledEvent.Should().BeNull();
    }
        
    [Fact]
    public void Seat_Sell_Success()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();

        seat.Reserve(userId, []);
        seat.ClearDomainEvents();
        seat.Sell(userId);
        
        seat.IsSold.Should().BeTrue();
        seat.UserId.Should().Be(userId);
    }

    [Fact]
    public void Seat_Sell_SeatCannotBeSoldRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();

        seat.Reserve(userId, []);
        seat.ClearDomainEvents();
        seat.Sell(userId);
        
        AssertBrokenRule<SeatCannotBeSoldRule>(() => seat.Sell(userId));

        seat.IsSold.Should().BeTrue();
        seat.UserId.Should().Be(userId);
    }
    
    [Fact]
    public void Seat_Sell_SeatHasToBeReservedByUserRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        
        AssertBrokenRule<SeatHasToBeReservedByUserRule>(() => seat.Sell(userId));

        seat.IsSold.Should().BeFalse();
    }
    
    [Fact]
    public void Seat_Sell_ReservationCannotExpiredRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();
        SetSystemClockForTest();

        seat.Reserve(userId, []);
        seat.ClearDomainEvents();
        
        SystemClock.Set(SystemClock.Now.AddMinutes(16));
        
        AssertBrokenRule<ReservationCannotExpiredRule>(() => seat.Sell(userId));

        seat.IsSold.Should().BeFalse();
    }
    
    [Fact]
    public void Seat_Sell_SeatCannotBeWithdrawnRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();

        seat.Withdrawn();
        seat.ClearDomainEvents();
        
        AssertBrokenRule<SeatCannotBeWithdrawnRule>(() => seat.Sell(userId));

        seat.IsSold.Should().BeFalse();
    }
    
    [Fact]
    public void Seat_Sell_SeatCannotBeSuspendedRule_RuleIsBroken()
    {
        var userId = Guid.NewGuid();
        var seat = CreateSeat();

        seat.Suspend();
        seat.ClearDomainEvents();
        
        AssertBrokenRule<SeatCannotBeSuspendedRule>(() => seat.Sell(userId));

        seat.IsSold.Should().BeFalse();
    }
    
    private Seat CreateSeat() => Seat.Create(_eventId, IsUnnumberedSeat, Sector, RowNumber, SeatNumber);
    private static void SetSystemClockForTest() => SystemClock.Set(new DateTime(2024, 03, 20, 20, 00, 00));

}