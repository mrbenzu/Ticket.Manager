using FluentAssertions;
using Ticket.Manager.Domain.Places;
using Ticket.Manager.Domain.UnitTests.Common;
using Xunit;

namespace Ticket.Manager.Domain.UnitTests.Places;

public class PlacesTests : TestBase
{
    private const string Name = "Test Place";
    private const string Street = "Test Street";
    private const string Number = "Test Number";
    private const string City = "Test City";
    private const int UnnumberedSectorCount = 1;
    private const int UnnumberedSeatsInSectorCount = 2;
    private const int SectorCount = 3;
    private const int RowCount = 4;
    private const int SeatsInRowsCount = 5;

    [Fact]
    public void Place_Create_Success()
    {
        // Arrange

        // Act
        var place = Place.Create(Name,
            Street, Number, City,
            UnnumberedSectorCount, UnnumberedSeatsInSectorCount,
            SectorCount, RowCount, SeatsInRowsCount);

        // Assert
        place.Name.Should().Be(Name);
        place.Address.Street.Should().Be(Street);
        place.Address.Number.Should().Be(Number);
        place.Address.City.Should().Be(City);
        place.UnnumberedSeatsMap.SectorCount.Should().Be(UnnumberedSectorCount);
        place.UnnumberedSeatsMap.SeatsInSectorCount.Should().Be(UnnumberedSeatsInSectorCount);
        place.SeatsMap.SectorCount.Should().Be(SectorCount);
        place.SeatsMap.RowsCount.Should().Be(RowCount);
        place.SeatsMap.SeatsInRowCount.Should().Be(SeatsInRowsCount);
    }
}