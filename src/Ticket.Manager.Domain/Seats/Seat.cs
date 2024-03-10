using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Seats;

public class Seat : IAggregateRoot
{
    private const int DefaultReservationTimeInMinutes = 15;
    private const int DefaultExtendedReservationTimeInMinutes = 10080;
    
    public Guid Id { get; private set; }
    
    public Guid EventId { get; private set; }
    
    public Guid OwnerId { get; private set; }
    
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
        OwnerId = Guid.Empty;
        SeatDetails = seatDetails;
        IsReserved = false;
        ReservedTo = DateTime.MinValue;
        IsSuspended = false;
        IsWithdrawn = false;
        IsSold = false;
    }

    public static Result<Seat> Create(Guid eventId, bool isNonNumeric, int sector, int rowNumber, int seatNumber)
    {
        var id = Guid.NewGuid();
        var seatDetails = new SeatDetails(isNonNumeric, sector, rowNumber, seatNumber);
        
        var seat = new Seat(id, eventId, seatDetails);
        
        return Result.Success(seat);
    }

    public Result Reserve(Guid ownerId, List<int> reservedSeatNumbersInRow)
    {
        if (OwnerId == ownerId)
        {
            return Result.Failure(SeatErrors.IsReserved);
        }
        
        if (IsReserved && ReservedTo > SystemClock.Now)
        {
            return Result.Failure(SeatErrors.IsReserved);
        }
        
        if (!SeatDetails.IsNonNumeric &&
            (reservedSeatNumbersInRow.Any(x => x == SeatDetails.SeatNumber + 2)  ||
            reservedSeatNumbersInRow.Any(x => x == SeatDetails.SeatNumber - 2)))
        {
            return Result.Failure(SeatErrors.CannotLeaveEmptySeatNearReserved);
        }
        
        if (IsSold)
        {
            return Result.Failure(SeatErrors.IsSold);
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
        OwnerId = ownerId;
        
        return Result.Success();
    }

    public Result ExtendReservationTime(Guid ownerId)
    {
        if (IsSold)
        {
            return Result.Failure(SeatErrors.IsSold);
        }
        
        if (OwnerId != ownerId)
        {
            return Result.Failure(SeatErrors.IsNotReservedByOwner);
        }
        
        ReservedTo = SystemClock.Now.AddMinutes(DefaultReservationTimeInMinutes);
        
        return Result.Success();
    }

    public Result CancelReservation()
    {
        IsReserved = false;
        OwnerId = Guid.Empty;
        
        return Result.Success();
    }
    
    public Result Sell(Guid ownerId)
    {
        if (IsSold)
        {
            return Result.Failure(SeatErrors.IsSold);
        }
        
        if (OwnerId == ownerId)
        {
            return Result.Failure(SeatErrors.IsNotReservedByOwner);
        }
        
        if (IsReserved && ReservedTo < SystemClock.Now)
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
        OwnerId = ownerId;
        
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
        OwnerId = Guid.Empty;
            
        return Result.Success();
    }

    public Result Withdrawn()
    {
        IsWithdrawn = true;
        IsReserved = false;
        IsSold = false;
        OwnerId = Guid.Empty;
        
        return Result.Success();
    }
    
    public Result Suspend()
    {
        if (IsWithdrawn)
        {
            return Result.Failure(SeatErrors.IsWithdrawn);
        }
        
        IsSuspended = true;
        
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
        
        return Result.Success();
    }
}