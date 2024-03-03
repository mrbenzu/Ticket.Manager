using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events;

public class Event : Entity, IAggregateRoot
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime StartOfSalesDate { get; private set; }
    
    public bool IsCanceled { get; private set; }
    
    public bool IsSuspended { get; private set; }
    
    private Event()
    {
        // EF
    }
}