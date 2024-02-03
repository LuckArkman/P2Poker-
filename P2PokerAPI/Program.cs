using P2Poker.Bean;
using P2Poker.Controller;
using P2Poker.Singletons;
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
//app.UseHttpsRedirection();
app.MapGet("/getroom", (u.Guid Id) =>
    {
        Room r = Singleton._singleton().GetRoom(Id);
        if (r is not null)
        {
            return r;
        }
        return null;
    })
    .WithName("getroom")
    .WithOpenApi();
app.MapGet("getallrooms", () =>
    {
        var r = Singleton._singleton().GetAllRoom();
        if (r is not null)
        {
            return r;
        }
        return null;
    })
    .WithName("getallroom")
    .WithOpenApi();
app.MapPost("createroom", (u.Guid Id) =>
    {
        var output = new Room();
        output.OnStart();
        Room r = Singleton._singleton().RegisterRoom(output);
        if (r is not null)
        {
            return r.UUID.ToString();
        }
        return null;
    })
    .WithName("createroom")
    .WithOpenApi();

app.Run();