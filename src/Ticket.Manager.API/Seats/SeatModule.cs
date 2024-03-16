using MediatR;
using Ticket.Manager.Application.Seats.Commands.CancelReservation;
using Ticket.Manager.Application.Seats.Commands.Reserve;
using Ticket.Manager.Application.Seats.Commands.WithdrawnSeat;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.API.Seats;

public static class SeatModule
{
    public static void AddSeatEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/reserve", async (ISender sender, ReserveSeatCommand command) =>
            {
                var result = await sender.Send(command);

                return Result(result);
            })
            .WithName("Reserve")
            .WithOpenApi();
        
        app.MapPost("/cancelReservation", async (ISender sender, CancelReservationCommand command) =>
            {
                var result = await sender.Send(command);

                return Result(result);
            })
            .WithName("CancelReservation")
            .WithOpenApi();
        
        app.MapPost("/withdrawnSeat", async (ISender sender, WithdrawnSeatCommand command) =>
            {
                var result = await sender.Send(command);

                return Result(result);
            })
            .WithName("WithdrawnSeat")
            .WithOpenApi();
    }

    private static IResult Result(Result result) =>
        result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error.Description);
}