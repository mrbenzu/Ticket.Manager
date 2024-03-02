using MediatR;

namespace Ticket.Manager.Domain.Common.Domain;

public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime CreatedAt { get; }
}