using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Orders.Events;

public record PaymentApprovedEvent(Guid OrderId) : DomainEvent;