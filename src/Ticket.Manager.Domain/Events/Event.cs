﻿using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Common.Domain;
using Ticket.Manager.Domain.Events.Events;

namespace Ticket.Manager.Domain.Events;

public class Event : Entity, IAggregateRoot
{
    public Guid Id { get; init; }
    
    public string Name { get; private set; }
    
    public long PlaceId { get; private set; }
    
    public SeatMap SeatMap { get; private set; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime StartOfSalesDate { get; private set; }
    
    public bool IsCanceled { get; private set; }
    
    public bool IsSuspended { get; private set; }
    
    private Event()
    {
        // EF
    }

    private Event(Guid id, string name, DateTime startDate, DateTime startOfSalesDate, long placeId, SeatMap seatMap)
    {
        Id = id;
        Name = name;
        StartDate = startDate;
        StartOfSalesDate = startOfSalesDate;
        PlaceId = placeId;
        SeatMap = seatMap;
        IsCanceled = false;
        IsSuspended = false;
    }
    
    public static Result<Event> Create(string name, DateTime startDate, DateTime startOfSalesDate, long placeId, SeatMap seatMap)
    {
        var id = Guid.NewGuid();
        
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Event>.Failure(EventErrors.InvalidName);
        }        
        
        if (startOfSalesDate < startDate)
        {
            return Result<Event>.Failure(EventErrors.StartOfSalesDateIsEarlierThanStartDate);
        }
        
        var @event = new Event(id, name, startDate, startOfSalesDate, placeId, seatMap);
        @event.AddDomainEvent(new EventCreatedDomainEvent(@event.Id, @event.SeatMap));

        return Result.Success(@event);
    }
    
    public Result Suspend()
    {
        if (IsCanceled)
        {
            return Result.Failure(EventErrors.IsCanceled);
        }
        
        IsSuspended = true;
        AddDomainEvent(new EventSuspendedEvent(Id));
        
        return Result.Success();
    }
    
    public Result Reopen()
    {
        if (IsCanceled)
        {
            return Result.Failure(EventErrors.IsCanceled);
        }
        
        IsSuspended = true;
        AddDomainEvent(new EventReopenedEvent(Id));
        
        return Result.Success();
    }
    
    public Result Cancel()
    {
        if (IsCanceled)
        {
            return Result.Failure(EventErrors.IsCanceled);
        }

        IsCanceled = true;
        AddDomainEvent(new EventCanceledEvent(Id));
        
        return Result.Success();
    }

    public Result ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(EventErrors.InvalidName);
        }

        Name = name;

        return Result.Success();
    }

    public Result ChangeStartDate(DateTime startDate)
    {
        StartDate = startDate;
    
        return Result.Success();
    }
    
    public Result ChangeStartOfSalesDate(DateTime startOfSalesDate)
    {
        return startOfSalesDate < StartDate 
            ? Result.Failure(EventErrors.StartOfSalesDateIsEarlierThanStartDate) 
            : Result.Success();
    }
}