using Ticket.Manager.Domain.Common.Domain;
using Ticket.Manager.Domain.Events.BusinessRules;
using Ticket.Manager.Domain.Events.Events;

namespace Ticket.Manager.Domain.Events;

public class Event : Entity, IAggregateRoot
{
    public Guid Id { get; init; }
    
    public string Name { get; private set; }
    
    public Guid PlaceId { get; private set; }
    
    public UnnumberedSeatsMap UnnumberedSeatsMap { get; private set; }
    
    public SeatsMap SeatsMap { get; private set; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime StartOfSalesDate { get; private set; }
    
    public bool IsCanceled { get; private set; }
    
    public bool IsSuspended { get; private set; }
    
    private Event()
    {
        // EF
    }

    private Event(Guid id, string name, DateTime startDate, DateTime startOfSalesDate, Guid placeId, UnnumberedSeatsMap unnumberedSeatsMap, SeatsMap seatsMap)
    {
        CheckRule(new EventNameCannotBeNullOrWhiteSpaceRule(name));
        CheckRule(new EventStartOfSalesDateCannotBeEarlierThanStartDateRule(startDate, startOfSalesDate));
        
        Id = id;
        Name = name;
        StartDate = startDate;
        StartOfSalesDate = startOfSalesDate;
        PlaceId = placeId;
        UnnumberedSeatsMap = unnumberedSeatsMap;
        SeatsMap = seatsMap;
        IsCanceled = false;
        IsSuspended = false;
        
        AddDomainEvent(new EventCreatedEvent(Id, UnnumberedSeatsMap, SeatsMap));
    }
    
    public static Event Create(string name, DateTime startDate, DateTime startOfSalesDate, Guid placeId, 
        int unnumberedSeatsSectorCount, int unnumberedSeatsInSectorCount,
        int sectorCount, int rowsCount, int seatsInRowCount)
    {
        var id = Guid.NewGuid();
        var unnumberedSeatsMap = new UnnumberedSeatsMap(unnumberedSeatsSectorCount, unnumberedSeatsInSectorCount);
        var seatsMap = new SeatsMap(sectorCount, rowsCount, seatsInRowCount);
        
        var @event = new Event(id, name, startDate, startOfSalesDate, placeId, unnumberedSeatsMap, seatsMap);

        return @event;
    }
    
    public void Suspend()
    {
        CheckRule(new EventCannotSuspendCanceledEventRule(IsCanceled));
        
        IsSuspended = true;
        AddDomainEvent(new EventSuspendedEvent(Id));
    }
    
    public void Reopen()
    {
        CheckRule(new EventCannotReopenCanceledEventRule(IsCanceled));
        
        IsSuspended = false;
        AddDomainEvent(new EventReopenedEvent(Id));
    }
    
    public void Cancel()
    {
        CheckRule(new EventCannotCancelCanceledEventRule(IsCanceled));

        IsCanceled = true;
        AddDomainEvent(new EventCanceledEvent(Id));
    }

    public void ChangeName(string name)
    {
        CheckRule(new EventNameCannotBeNullOrWhiteSpaceRule(name));

        Name = name;
    }

    public void ChangeStartDate(DateTime startDate)
    {
        StartDate = startDate;
    }
    
    public void ChangeStartOfSalesDate(DateTime startOfSalesDate)
    {
        CheckRule(new EventStartOfSalesDateCannotBeEarlierThanStartDateRule(StartDate, startOfSalesDate));

        StartOfSalesDate = startOfSalesDate;
    }
}