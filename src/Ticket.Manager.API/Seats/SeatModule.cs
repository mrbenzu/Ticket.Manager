using MediatR;
using Ticket.Manager.Application.Seats.Commands.CancelReservation;
using Ticket.Manager.Application.Seats.Commands.Reserve;
using Ticket.Manager.Application.Seats.Commands.WithdrawnSeat;

namespace Ticket.Manager.API.Seats;

public static class SeatModule
{
    public static void AddSeatEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/seat/reserve", async (ISender sender, ReserveSeatCommand command) => await sender.Send(command))
            .WithName("Seat Reserve")
            .WithOpenApi();
        
        app.MapPost("/seat/cancelReservation", async (ISender sender, CancelReservationCommand command) => await sender.Send(command))
            .WithName("Seat Cancel Reservation")
            .WithOpenApi();
        
        app.MapPost("/seat/withdrawnSeat", async (ISender sender, WithdrawnSeatCommand command) => await sender.Send(command))
            .WithName("SeatWithdrawn Seat")
            .WithOpenApi();
    }
}