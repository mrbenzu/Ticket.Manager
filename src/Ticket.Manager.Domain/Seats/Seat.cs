using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Common.Domain;
using Ticket.Manager.Domain.Seats.BusinessRules;
using Ticket.Manager.Domain.Seats.Events;

namespace Ticket.Manager.Domain.Seats;

public class Seat : Entity, IAggregateRoot
{
    private const int DefaultReservationTimeInMinutes = 15;
    private const int DefaultExtendedReservationTimeInMinutes = 10080;
    
    public Guid Id { get; private set; }
    
    public Guid EventId { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public SeatDetails SeatDetails { get; private set; }
    
    public bool IsReserved { get; private set; }
    
    public DateTime ReservedTo { get; private set; }
    
    public bool IsSuspended { get; private set; }
    
    public bool IsWithdrawn { get; private set; }
    
    public bool IsSold { get; private set; }
    
    private Seat()
    {
        // EF
    }
    
    private Seat(Guid id, Guid eventId, SeatDetails seatDetails)
    {
        Id = id;
        EventId = eventId;
        UserId = Guid.Empty;
        SeatDetails = seatDetails;
        IsReserved = false;
        ReservedTo = DateTime.MinValue;
        IsSuspended = false;
        IsWithdrawn = false;
        IsSold = false;
    }

    public static Seat Create(Guid eventId, bool isUnnumberedSeat, int sector, int rowNumber, int seatNumber)
    {
        var id = Guid.NewGuid();
        var seatDetails = new SeatDetails(isUnnumberedSeat, sector, rowNumber, seatNumber);
        
        var seat = new Seat(id, eventId, seatDetails);
        
        return seat;
    }

    public void Reserve(Guid userId, List<int> reservedSeatNumbersInRow, bool isLongTimeReservation = false)
    {
        CheckRule(new SeatCannotBeAlreadyReservedByUserRule(UserId, userId));
        CheckRule(new SeatCannotBeReservedRule(IsReserved, ReservedTo));
        CheckRule(new CannotLeaveEmptySeatNearReservedRule(SeatDetails,  reservedSeatNumbersInRow));
        CheckRule(new SeatCannotBeSoldRule(IsSold));
        CheckRule(new SeatCannotBeWithdrawnRule(IsWithdrawn));
        CheckRule(new SeatCannotBeSuspendedRule(IsSuspended));
        
        IsReserved = true;
        ReservedTo = isLongTimeReservation
            ? SystemClock.Now.AddMinutes(DefaultReservationTimeInMinutes)
            : SystemClock.Now.AddMinutes(DefaultExtendedReservationTimeInMinutes);
        UserId = userId;
        
        AddDomainEvent(new SeatReservedEvent(Id));
    }
    
    public void ExtendReservationTime(Guid userId)
    {
        CheckRule(new SeatCannotBeSoldRule(IsSold));
        CheckRule(new SeatHasToBeReservedByUserRule(UserId, userId));
        
        ReservedTo = SystemClock.Now.AddMinutes(DefaultReservationTimeInMinutes);
    }

    public void CancelReservation(Guid userId)
    {
        CheckRule(new SeatHasToBeReservedByUserRule(UserId, userId));
        
        IsReserved = false;
        UserId = Guid.Empty;
        
        AddDomainEvent(new ReservationCanceledEvent(Id));
    }
    
    public void Sell(Guid userId)
    {
        CheckRule(new SeatCannotBeSoldRule(IsSold));
        CheckRule(new SeatHasToBeReservedByUserRule(UserId, userId)); 
        CheckRule(new ReservationCannotExpiredRule(IsReserved, ReservedTo));
        CheckRule(new SeatCannotBeWithdrawnRule(IsWithdrawn));
        CheckRule(new SeatCannotBeSuspendedRule(IsSuspended));
        
        IsSold = true;
        UserId = userId;
    }
    
    public void Return()
    {
        CheckRule(new SeatHasToBeSold(IsSold));
        CheckRule(new SeatCannotBeWithdrawnRule(IsWithdrawn));
        CheckRule(new SeatCannotBeSuspendedRule(IsSuspended));
    
        IsReserved = false;
        IsSold = false;
        UserId = Guid.Empty;
    }

    public void Withdrawn()
    {
        var @event = new SeatWithdrawnEvent(Id, EventId, UserId, IsReserved, 
            SeatDetails.IsUnnumberedSeat, SeatDetails.Sector, SeatDetails.RowNumber, SeatDetails.SeatNumber);
        
        IsWithdrawn = true;
        IsSuspended = false;
        IsReserved = false;
        ReservedTo = DateTime.MinValue;
        IsSold = false;
        UserId = Guid.Empty;
        
        AddDomainEvent(@event);
    }
    
    public void Suspend()
    {
        CheckRule(new SeatCannotBeWithdrawnRule(IsWithdrawn));

        if (ReservedTo >= SystemClock.Now)
        {
            IsReserved = false;
            ReservedTo = DateTime.MinValue;
            AddDomainEvent(new ReservationCanceledEvent(Id));
        }
        
        IsSuspended = true;
        AddDomainEvent(new SeatSuspendedEvent(Id));
    }

    public void Reopen()
    {
        CheckRule(new SeatCannotBeWithdrawnRule(IsWithdrawn));
        CheckRule(new CannotReopenNotSuspendedSeatRule(IsSuspended));
 
        IsSuspended = false;
        AddDomainEvent(new SeatReopenedEvent(Id));
    }
}