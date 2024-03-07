using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Places;

public record Address(string Street, string Number, string City) : ValueObject;