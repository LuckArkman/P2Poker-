using P2Poker.Controller;
using P2PokerAPI;
using u = System;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
new Startup().StartHost();
builder.Services.AddAndConfigureControllers();
builder.Services.DBConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapGet("/getroom", (u.Guid Id) =>
    {
        return Id;
    })
    .WithName("getroom")
    .WithOpenApi();

app.MapPost("/createroom", (u.Guid Id) =>
    {
        return u.Guid.NewGuid();
    })
    .WithName("createroom")
    .WithOpenApi();

app.Run();