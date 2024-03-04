namespace Ticket.Manager.Domain.Common;

public record Error(string Code, string? Description = null)
{
    public static readonly Error None = new Error(string.Empty);
}