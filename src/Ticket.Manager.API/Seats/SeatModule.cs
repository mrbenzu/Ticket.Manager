using MediatR;
using Ticket.Manager.Application.Seats.Commands.CancelReservation;
using Ticket.Manager.Application.Seats.Commands.Reserve;
using Ticket.Manager.Application.Seats.Commands.WithdrawnSeat;

namespace Ticket.Manager.API.Seats;

public static class SeatModule
{
    public static void AddSeatEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/reserve", async (ISender sender, ReserveSeatCommand command) => await sender.Send(command))
            .WithName("Reserve")
            .WithOpenApi();
        
        app.MapPost("/cancelReservation", async (ISender sender, CancelReservationCommand command) => await sender.Send(command))
            .WithName("CancelReservation")
            .WithOpenApi();
        
        app.MapPost("/withdrawnSeat", async (ISender sender, WithdrawnSeatCommand command) => await sender.Send(command))
            .WithName("WithdrawnSeat")
            .WithOpenApi();
    }
}