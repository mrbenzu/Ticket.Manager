using MediatR;
using Ticket.Manager.Application.Places.CreatePlace;

namespace Ticket.Manager.API.Places;

public static class PlaceModule
{
    public static void AddPlaceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/place/create", async (ISender sender, CreatePlaceCommand command) => await sender.Send(command))
            .WithName("Place Create")
            .WithOpenApi();
    }
}