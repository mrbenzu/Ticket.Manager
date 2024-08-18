using Ticket.Manager.API.Events;
using Ticket.Manager.API.Orders;
using Ticket.Manager.API.Places;
using Ticket.Manager.API.Seats;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddEventEndpoints();
app.AddOrderEndpoints();
app.AddPlaceEndpoints();
app.AddSeatEndpoints();

app.Run();
