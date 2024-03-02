namespace Ticket.Manager.Domain.Common.Domain;

public record DomainEvent : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = SystemClock.Now;
}