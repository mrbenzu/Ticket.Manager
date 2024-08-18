using MediatR;
using Ticket.Manager.Application.Orders.Commands.CreateOrder;
using Ticket.Manager.Application.Orders.Commands.ReturnOrder;

namespace Ticket.Manager.API.Orders;

public static class OrderModule
{
    public static void AddOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/order/create", async (ISender sender, CreateOrderCommand command) => await sender.Send(command))
            .WithName("Order Create")
            .WithOpenApi();
        
        app.MapPost("/order/return", async (ISender sender, ReturnOrderCommand command) => await sender.Send(command))
            .WithName("Order Return")
            .WithOpenApi();
    }
}