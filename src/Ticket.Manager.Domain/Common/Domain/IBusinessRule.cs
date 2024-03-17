namespace Ticket.Manager.Domain.Common.Domain;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}