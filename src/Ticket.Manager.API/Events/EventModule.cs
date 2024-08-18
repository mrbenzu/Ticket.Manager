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
        app.MapPost("/event/create", async (ISender sender, CreateEventCommand command) => await sender.Send(command))
            .WithName("Event Create")
            .WithOpenApi();
        
        app.MapPost("/event/cancel", async (ISender sender, CancelEventCommand command) => await sender.Send(command))
            .WithName("Event Cancel")
            .WithOpenApi();
        
        app.MapPost("/event/suspend", async (ISender sender, SuspendEventCommand command) => await sender.Send(command))
            .WithName("Event Suspend")
            .WithOpenApi();
        
        app.MapPost("/event/reopen", async (ISender sender, ReopenEventCommand command) => await sender.Send(command))
            .WithName("Event Reopen")
            .WithOpenApi();
        
        app.MapPost("/event/changeName", async (ISender sender, ChangeNameCommand command) => await sender.Send(command))
            .WithName("Event Change Name")
            .WithOpenApi();
        
        app.MapPost("/event/changeStartDate", async (ISender sender, ChangeStartDateCommand command) => await sender.Send(command))
            .WithName("Event Change Start Date")
            .WithOpenApi();
        
        app.MapPost("/event/changeStartOfSalesDate", async (ISender sender, ChangeStartOfSalesDateCommand command) => await sender.Send(command))
            .WithName("Event Change Start Of Sales Date")
            .WithOpenApi();
    }
}