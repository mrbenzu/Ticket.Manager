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
        private readonly DateTime _startDate = new(2024, 03, 17);
        private readonly DateTime _startOfSalesDate = new(2024, 03, 18);
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
        
        [Fact]
        public void Event_Create_InvalidName_Failed()
        {
            var result = Event.Create(null, _startDate, _startOfSalesDate, _placeId,
                UnnumberedSeatsSectorCount, UnnumberedSeatsInSectorCount,
                SectorCount, RowsCount, SeatsInRowCount);
            
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(EventErrors.InvalidName);
        }
        
        [Fact]
        public void Event_Create_StartOfSalesDateIsEarlierThanStartDate_Failed()
        {
            var startOfSalesDate = new DateTime(2024, 03, 17);
            var startDate = startOfSalesDate.AddDays(1);

            var result = Event.Create(EventName, startDate, startOfSalesDate, _placeId,
                UnnumberedSeatsSectorCount, UnnumberedSeatsInSectorCount,
                SectorCount, RowsCount, SeatsInRowCount);
            
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(EventErrors.StartOfSalesDateIsEarlierThanStartDate);
        }
        
        [Fact]
        public void Event_Suspend_Success()
        {
            var result = CreateEvent();
            
            result.Value.Suspend();
            
            result.Value.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.IsSuspended.Should().BeTrue();

            var eventCreatedEvent = GetDomainEvent<EventSuspendedEvent>(result.Value);
            eventCreatedEvent.Should().NotBeNull();
            eventCreatedEvent.EventId.Should().Be(result.Value.Id);
        }
        
        [Fact]
        public void Event_Suspend_Failed()
        {
            var result = CreateEvent();

            result.Value.Cancel();
            var res = result.Value.Suspend();
            
            res.IsSuccess.Should().BeFalse();
            res.Error.Should().Be(EventErrors.IsCanceled);
        }
        
        [Fact]
        public void Event_Reopen_Success()
        {
            var result = CreateEvent();
            
            result.Value.Reopen();
            
            result.Value.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.IsSuspended.Should().BeFalse();
            
            var eventReopenedEvent = GetDomainEvent<EventReopenedEvent>(result.Value);
            eventReopenedEvent.Should().NotBeNull();
            eventReopenedEvent.EventId.Should().Be(result.Value.Id);
        }
        
        [Fact]
        public void Event_Reopen_Failed()
        {
            var result = CreateEvent();

            result.Value.Cancel();
            var res = result.Value.Reopen();
            
            res.IsSuccess.Should().BeFalse();
            res.Error.Should().Be(EventErrors.IsCanceled);
        }
        
        [Fact]
        public void Event_Cancel_Success()
        {
            var result = CreateEvent();
            
            result.Value.Cancel();
            
            result.Value.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            
            var eventCanceledEvent = GetDomainEvent<EventCanceledEvent>(result.Value);
            eventCanceledEvent.Should().NotBeNull();
            eventCanceledEvent.EventId.Should().Be(result.Value.Id);
        }
        
        [Fact]
        public void Event_Cancel_Failed()
        {
            var result = CreateEvent();

            result.Value.Cancel();
            var res = result.Value.Cancel();
            
            res.IsSuccess.Should().BeFalse();
            res.Error.Should().Be(EventErrors.IsCanceled);
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