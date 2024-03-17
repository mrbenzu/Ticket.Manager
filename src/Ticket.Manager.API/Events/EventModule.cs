using MediatR;
using Ticket.Manager.Application.Events.Commands.Cancel;
using Ticket.Manager.Application.Events.Commands.ChangeName;
using Ticket.Manager.Application.Events.Commands.ChangeStartDate;
using Ticket.Manager.Application.Events.Commands.ChangeStartOfSalesDate;
using Ticket.Manager.Application.Events.Commands.Create;
using Ticket.Manager.Application.Events.Commands.Reopen;
using Ticket.Manager.Application.Events.Commands.Suspend;

namespace Ticket.Manager.API.Events;

public static class EventModule
{
    public static void AddEventEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async (ISender sender, CreateEventCommand command) => await sender.Send(command))
            .WithName("Create")
            .WithOpenApi();
        
        app.MapPost("/cancel", async (ISender sender, CancelEventCommand command) => await sender.Send(command))
            .WithName("Cancel")
            .WithOpenApi();
        
        app.MapPost("/suspend", async (ISender sender, SuspendEventCommand command) => await sender.Send(command))
            .WithName("Suspend")
            .WithOpenApi();
        
        app.MapPost("/reopen", async (ISender sender, ReopenEventCommand command) => await sender.Send(command))
            .WithName("Reopen")
            .WithOpenApi();
        
        app.MapPost("/changeName", async (ISender sender, ChangeNameCommand command) => await sender.Send(command))
            .WithName("ChangeName")
            .WithOpenApi();
        
        app.MapPost("/changeStartDate", async (ISender sender, ChangeStartDateCommand command) => await sender.Send(command))
            .WithName("ChangeStartDate")
            .WithOpenApi();
        
        app.MapPost("/changeStartOfSalesDate", async (ISender sender, ChangeStartOfSalesDateCommand command) => await sender.Send(command))
            .WithName("ChangeStartOfSalesDate")
            .WithOpenApi();
    }
}