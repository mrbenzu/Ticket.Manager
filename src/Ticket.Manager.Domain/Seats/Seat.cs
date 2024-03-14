using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Common.Domain;
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

    public static Result<Seat> Create(Guid eventId, bool isUnnumberedSeat, int sector, int rowNumber, int seatNumber)
    {
        var id = Guid.NewGuid();
        var seatDetails = new SeatDetails(isUnnumberedSeat, sector, rowNumber, seatNumber);
        
        var seat = new Seat(id, eventId, seatDetails);
        
        return Result.Success(seat);
    }

    public Result Reserve(Guid userId, List<int> reservedSeatNumbersInRow)
    {
        if (UserId == userId)
        {
            return Result.Failure(SeatErrors.IsAlreadyReservedByUser);
        }
        
        if (IsReservationValid())
        {
            return Result.Failure(SeatErrors.IsReserved);
        }
        
        if (IsNotEmptySpaceBetweenNearSeats(reservedSeatNumbersInRow))
        {
            return Result.Failure(SeatErrors.CannotLeaveEmptySeatNearReserved);
        }
        
        if (IsSold)
        {
            return Result.Failure(SeatErrors.IsAlreadySold);
        }
        
        if (IsWithdrawn)
        {
            return Result.Failure(SeatErrors.IsWithdrawn);
        }
        
        if (IsSuspended)
        {
            return Result.Failure(SeatErrors.IsSuspended);
        }
        
        IsReserved = true;
        ReservedTo = SystemClock.Now.AddMinutes(DefaultReservationTimeInMinutes);
        UserId = userId;
        
        AddDomainEvent(new SeatReservedEvent(Id));
        
        return Result.Success();
    }

    private bool IsNotEmptySpaceBetweenNearSeats(IReadOnlyCollection<int> reservedSeatNumbersInRow)
    {
        return !SeatDetails.IsUnnumberedSeat &&
               (reservedSeatNumbersInRow.Any(x => x == SeatDetails.SeatNumber + 2)  ||
                reservedSeatNumbersInRow.Any(x => x == SeatDetails.SeatNumber - 2));
    }

    public Result ExtendReservationTime(Guid userId)
    {
        if (IsSold)
        {
            return Result.Failure(SeatErrors.IsAlreadySold);
        }
        
        if (UserId != userId)
        {
            return Result.Failure(SeatErrors.IsNotReservedByUser);
        }
        
        ReservedTo = SystemClock.Now.AddMinutes(DefaultReservationTimeInMinutes);
        
        return Result.Success();
    }

    public Result CancelReservation(Guid userId)
    {
        if (UserId != userId)
        {
            return Result.Failure(SeatErrors.IsNotReservedByUser);
        }
        
        IsReserved = false;
        UserId = Guid.Empty;
        
        AddDomainEvent(new ReservationCanceledEvent(Id));
        
        return Result.Success();
    }
    
    public Result Sell(Guid userId)
    {
        if (IsSold)
        {
            return Result.Failure(SeatErrors.IsAlreadySold);
        }
        
        if (UserId == userId)
        {
            return Result.Failure(SeatErrors.IsNotReservedByUser);
        }
        
        if (IsReservationValid())
        {
            return Result.Failure(SeatErrors.ReservationExpired);
        }
        
        if (IsSuspended)
        {
            return Result.Failure(SeatErrors.IsSuspended);
        }
        
        if (IsWithdrawn)
        {
            return Result.Failure(SeatErrors.IsWithdrawn);
        }
        
        IsSold = true;
        UserId = userId;
        
        return Result.Success();
    }
    
    public Result Return()
    {
        if (!IsSold)
        {
            return Result.Failure(SeatErrors.IsNotSold);
        }
    
        IsReserved = false;
        IsSold = false;
        UserId = Guid.Empty;
            
        return Result.Success();
    }

    public Result Withdrawn()
    {
        IsWithdrawn = true;
        IsSuspended = false;
        IsReserved = false;
        ReservedTo = DateTime.MinValue;
        IsSold = false;
        UserId = Guid.Empty;
        
        return Result.Success();
    }
    
    public Result Suspend()
    {
        if (IsWithdrawn)
        {
            return Result.Failure(SeatErrors.IsWithdrawn);
        }

        if (IsReservationValid())
        {
            IsReserved = false;
            ReservedTo = DateTime.MinValue;
            AddDomainEvent(new ReservationCanceledEvent(Id));
        }
        
        IsSuspended = true;
        AddDomainEvent(new SeatSuspendedEvent(Id));
        
        return Result.Success();
    }

    public Result Reopen()
    {
        if (IsWithdrawn)
        {
            return Result.Failure(SeatErrors.IsWithdrawn);
        }
        
        if (!IsSuspended)
        {
            return Result.Failure(SeatErrors.IsNotSuspended);
        }
        
        IsSuspended = false;
        AddDomainEvent(new SeatReopenedEvent(Id));
        
        return Result.Success();
    }
    
    private bool IsReservationValid()
    {
        return IsReserved && ReservedTo > SystemClock.Now;
    }
}