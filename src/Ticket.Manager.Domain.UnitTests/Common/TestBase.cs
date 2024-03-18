using FluentAssertions;
using Ticket.Manager.Domain.Common.Domain;
using Xunit;

namespace Ticket.Manager.Domain.UnitTests.Common;

public class TestBase
{
    protected static T? GetDomainEvent<T>(Entity entity) where T : DomainEvent
    {
        var domainEvents = entity.DomainEvents;
        var domainEvent = domainEvents.OfType<T>().FirstOrDefault();
        return domainEvent;
    }
        
    protected static void AssertBrokenRule<TRule>(Func<object> action)
        where TRule : class, IBusinessRule
    {
        var exception = Assert.Throws<BusinessRuleValidationException>(action);
        exception.BrokenRule.Should().BeOfType<TRule>();
    }
        
    protected static void AssertBrokenRule<TRule>(Action action)
        where TRule : class, IBusinessRule
    {
        var exception = Assert.Throws<BusinessRuleValidationException>(action);
        exception.BrokenRule.Should().BeOfType<TRule>();
    }
}