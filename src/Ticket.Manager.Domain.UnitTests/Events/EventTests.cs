using FluentAssertions;
using Ticket.Manager.Domain.Common;
using Ticket.Manager.Domain.Common.Domain;
using Ticket.Manager.Domain.Events;
using Ticket.Manager.Domain.Events.Events;
using Xunit;

namespace Ticket.Manager.Domain.UnitTests.Events
{
    public class EventTests
    {
        private const string EventName = "Test Event";
        private readonly DateTime _startDate = new DateTime(2024, 03, 17);
        private readonly DateTime _startOfSalesDate = new DateTime(2024, 03, 18);
        private readonly Guid _placeId = Guid.NewGuid();
        private const int UnnumberedSeatsSectorCount = 2;
        private const int UnnumberedSeatsInSectorCount = 100;
        private const int SectorCount = 10;
        private const int RowsCount = 11;
        private const int SeatsInRowCount = 12;

        [Fact]
        public void Event_Create_Success()
        {
            var result = CreateEvent();
            
            result.Value.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be(EventName);
            result.Value.StartDate.Should().Be(_startDate);
            result.Value.StartOfSalesDate.Should().Be(_startOfSalesDate);
            result.Value.PlaceId.Should().Be(_placeId);
            result.Value.UnnumberedSeatsMap.SectorCount.Should().Be(UnnumberedSeatsSectorCount);
            result.Value.UnnumberedSeatsMap.SeatsInSectorCount.Should().Be(UnnumberedSeatsInSectorCount);
            result.Value.SeatsMap.SectorCount.Should().Be(SectorCount);
            result.Value.SeatsMap.RowsCount.Should().Be(RowsCount);
            result.Value.SeatsMap.SeatsInRowCount.Should().Be(SeatsInRowCount);

            var eventCreatedEvent = GetDomainEvent<EventCreatedEvent>(result.Value);
            eventCreatedEvent.Should().NotBeNull();
            eventCreatedEvent.EventId.Should().Be(result.Value.Id);
            eventCreatedEvent.UnnumberedSeatsMap.Should().BeEquivalentTo(result.Value.UnnumberedSeatsMap);
            eventCreatedEvent.SeatsMap.Should().BeEquivalentTo(result.Value.SeatsMap);
        }
        
        private Result<Event> CreateEvent()
        {
            var result = Event.Create(EventName, _startDate, _startOfSalesDate, _placeId,
                UnnumberedSeatsSectorCount, UnnumberedSeatsInSectorCount,
                SectorCount, RowsCount, SeatsInRowCount);

            return result;
        }

        private static T? GetDomainEvent<T>(Entity entity) where T : DomainEvent
        {
            var domainEvents = entity.DomainEvents;
            var domainEvent = domainEvents.OfType<T>().FirstOrDefault();
            return domainEvent;
        }
    }
}