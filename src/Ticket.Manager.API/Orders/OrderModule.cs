using MediatR;
using Ticket.Manager.Application.Orders.Commands.CreateOrder;
using Ticket.Manager.Application.Orders.Commands.ReturnOrder;

namespace Ticket.Manager.API.Orders;

public static class OrderModule
{
    public static void AddOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async (ISender sender, CreateOrderCommand command) => await sender.Send(command))
            .WithName("Create")
            .WithOpenApi();
        
        app.MapPost("/return", async (ISender sender, ReturnOrderCommand command) => await sender.Send(command))
            .WithName("Return")
            .WithOpenApi();
    }
}