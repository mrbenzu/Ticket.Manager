using FluentAssertions;
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
            var @event = CreateEvent();
            
             @event.Should().NotBeNull();
             @event.Name.Should().Be(EventName);
             @event.StartDate.Should().Be(_startDate);
             @event.StartOfSalesDate.Should().Be(_startOfSalesDate);
             @event.PlaceId.Should().Be(_placeId);
             @event.UnnumberedSeatsMap.SectorCount.Should().Be(UnnumberedSeatsSectorCount);
             @event.UnnumberedSeatsMap.SeatsInSectorCount.Should().Be(UnnumberedSeatsInSectorCount);
             @event.SeatsMap.SectorCount.Should().Be(SectorCount);
             @event.SeatsMap.RowsCount.Should().Be(RowsCount);
             @event.SeatsMap.SeatsInRowCount.Should().Be(SeatsInRowCount);

            var eventCreatedEvent = GetDomainEvent<EventCreatedEvent>( @event);
            eventCreatedEvent.Should().NotBeNull();
            eventCreatedEvent.EventId.Should().Be(@event.Id);
            eventCreatedEvent.UnnumberedSeatsMap.Should().BeEquivalentTo( @event.UnnumberedSeatsMap);
            eventCreatedEvent.SeatsMap.Should().BeEquivalentTo( @event.SeatsMap);
        }
        
        // [Fact]
        // public void Event_Create_InvalidName_Failed()
        // {
        //     var result = Event.Create(null, _startDate, _startOfSalesDate, _placeId,
        //         UnnumberedSeatsSectorCount, UnnumberedSeatsInSectorCount,
        //         SectorCount, RowsCount, SeatsInRowCount);
        //     
        //     result.IsSuccess.Should().BeFalse();
        //     result.Error.Should().Be(EventErrors.InvalidName);
        // }
        
        // [Fact]
        // public void Event_Create_StartOfSalesDateIsEarlierThanStartDate_Failed()
        // {
        //     var startOfSalesDate = new DateTime(2024, 03, 17);
        //     var startDate = startOfSalesDate.AddDays(1);
        //
        //     var result = Event.Create(EventName, startDate, startOfSalesDate, _placeId,
        //         UnnumberedSeatsSectorCount, UnnumberedSeatsInSectorCount,
        //         SectorCount, RowsCount, SeatsInRowCount);
        //     
        //     result.IsSuccess.Should().BeFalse();
        //     result.Error.Should().Be(EventErrors.StartOfSalesDateIsEarlierThanStartDate);
        // }
        
        // [Fact]
        // public void Event_Suspend_Success()
        // {
        //     var result = CreateEvent();
        //     
        //      @event.Suspend();
        //     
        //      @event.Should().NotBeNull();
        //     result.IsSuccess.Should().BeTrue();
        //      @event.IsSuspended.Should().BeTrue();
        //
        //     var eventCreatedEvent = GetDomainEvent<EventSuspendedEvent>( @event);
        //     eventCreatedEvent.Should().NotBeNull();
        //     eventCreatedEvent.EventId.Should().Be( @event.Id);
        // }
        //
        // [Fact]
        // public void Event_Suspend_Failed()
        // {
        //     var result = CreateEvent();
        //
        //      @event.Cancel();
        //     var res =  @event.Suspend();
        //     
        //     res.IsSuccess.Should().BeFalse();
        //     res.Error.Should().Be(EventErrors.IsCanceled);
        // }
        //
        // [Fact]
        // public void Event_Reopen_Success()
        // {
        //     var result = CreateEvent();
        //     
        //      @event.Reopen();
        //     
        //      @event.Should().NotBeNull();
        //     result.IsSuccess.Should().BeTrue();
        //      @event.IsSuspended.Should().BeFalse();
        //     
        //     var eventReopenedEvent = GetDomainEvent<EventReopenedEvent>( @event);
        //     eventReopenedEvent.Should().NotBeNull();
        //     eventReopenedEvent.EventId.Should().Be( @event.Id);
        // }
        //
        // [Fact]
        // public void Event_Reopen_Failed()
        // {
        //     var result = CreateEvent();
        //
        //      @event.Cancel();
        //     var res =  @event.Reopen();
        //     
        //     res.IsSuccess.Should().BeFalse();
        //     res.Error.Should().Be(EventErrors.IsCanceled);
        // }
        //
        // [Fact]
        // public void Event_Cancel_Success()
        // {
        //     var result = CreateEvent();
        //     
        //      @event.Cancel();
        //     
        //      @event.Should().NotBeNull();
        //     result.IsSuccess.Should().BeTrue();
        //     
        //     var eventCanceledEvent = GetDomainEvent<EventCanceledEvent>( @event);
        //     eventCanceledEvent.Should().NotBeNull();
        //     eventCanceledEvent.EventId.Should().Be( @event.Id);
        // }
        //
        // [Fact]
        // public void Event_Cancel_Failed()
        // {
        //     var result = CreateEvent();
        //
        //      @event.Cancel();
        //     var res =  @event.Cancel();
        //     
        //     res.IsSuccess.Should().BeFalse();
        //     res.Error.Should().Be(EventErrors.IsCanceled);
        // }
        
        private Event CreateEvent()
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