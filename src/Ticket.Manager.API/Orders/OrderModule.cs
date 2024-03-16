using MediatR;
using Ticket.Manager.Application.Orders.Commands.CreateOrder;
using Ticket.Manager.Application.Orders.Commands.ReturnOrder;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.API.Orders;

public static class OrderModule
{
    public static void AddOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async (ISender sender, CreateOrderCommand command) =>
            {
                var result = await sender.Send(command);

                return Result(result);
            })
            .WithName("Create")
            .WithOpenApi();
        
        app.MapPost("/return", async (ISender sender, ReturnOrderCommand command) =>
            {
                var result = await sender.Send(command);

                return Result(result);
            })
            .WithName("Return")
            .WithOpenApi();
    }

    private static IResult Result(Result result) =>
        result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error.Description);
}