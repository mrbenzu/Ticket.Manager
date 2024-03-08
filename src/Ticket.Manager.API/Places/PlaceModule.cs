using MediatR;
using Ticket.Manager.Application.Places;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.API.Places;

public static class PlaceModule
{
    public static void AddPlaceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async (ISender sender, CreatePlaceCommand command) =>
            {
                var result = await sender.Send(command);

                return Result(result);
            })
            .WithName("Create")
            .WithOpenApi();
    }

    private static IResult Result(Result result) =>
        result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error.Description);
}