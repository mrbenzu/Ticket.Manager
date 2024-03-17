using Ticket.Manager.Domain.Common.Domain;

namespace Ticket.Manager.Domain.Events.BusinessRules;

public class EventStartOfSalesDateCannotBeEarlierThanStartDateRule(DateTime startDate, DateTime startOfSalesDate) : IBusinessRule
{
    public bool IsBroken() => startOfSalesDate < startDate;
    
    public string Message => "Start of sales date cannot be earlier than the event start date.";
}