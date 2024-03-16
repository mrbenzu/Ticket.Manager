using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ticket.Manager.Domain.Events;
using Ticket.Manager.Domain.Orders;
using Ticket.Manager.Domain.Places;

namespace Ticket.Manager.Infrastructure.Configuration;

public class TicketManagerDbContext(DbContextOptions<TicketManagerDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Place> Places => Set<Place>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Order> Orders => Set<Order>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}