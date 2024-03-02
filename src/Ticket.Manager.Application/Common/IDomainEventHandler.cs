using MediatR;
using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Application.Common;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent;