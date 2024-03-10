﻿using MediatR;
using Ticket.Manager.Application.Events.Cancel;
using Ticket.Manager.Application.Events.Create;
using Ticket.Manager.Application.Events.Reopen;
using Ticket.Manager.Application.Events.Suspend;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.API.Events;

public static class EventModule
{
    public static void AddEventEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async (ISender sender, CreateEventCommand command) =>
            {
                var result = await sender.Send(command);

                return Result(result);
            })
            .WithName("Create")
            .WithOpenApi();
        
        app.MapPost("/cancel", async (ISender sender, CancelEventCommand command) =>
            {
                var result = await sender.Send(command);

                return Result(result);
            })
            .WithName("Cancel")
            .WithOpenApi();
        
        app.MapPost("/suspend", async (ISender sender, SuspendEventCommand command) =>
            {
                var result = await sender.Send(command);

                return Result(result);
            })
            .WithName("Suspend")
            .WithOpenApi();
        
        app.MapPost("/reopen", async (ISender sender, ReopenEventCommand command) =>
            {
                var result = await sender.Send(command);

                return Result(result);
            })
            .WithName("Reopen")
            .WithOpenApi();
    }

    private static IResult Result(Result result) =>
        result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error.Description);
}