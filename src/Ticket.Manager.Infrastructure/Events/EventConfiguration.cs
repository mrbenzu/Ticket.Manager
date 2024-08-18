using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Manager.Domain.Events;

namespace Ticket.Manager.Infrastructure.Events;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.PlaceId)
            .IsRequired();

        builder.ComplexProperty(e => e.UnnumberedSeatsMap);
        
        builder.ComplexProperty(e => e.SeatsMap);

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.StartOfSalesDate)
            .IsRequired();

        builder.Property(e => e.IsCanceled)
            .IsRequired();

        builder.Property(e => e.IsSuspended)
            .IsRequired();
    }
}