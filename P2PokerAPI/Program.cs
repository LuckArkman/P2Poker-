using u = System;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapGet("/getroom", (Guid Id) =>
    {
        return Id;
    })
    .WithName("getroom")
    .WithOpenApi();

app.MapPost("/createroom", (Guid Id) =>
    {
        return u.Guid.NewGuid();
    })
    .WithName("createroom")
    .WithOpenApi();

app.Run();