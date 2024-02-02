using P2Poker.Context;

namespace P2Poker.Controller;

public static class ControllersConfiguration
{
    public static IServiceCollection AddAndConfigureControllers(this IServiceCollection service)
    {
        service.AddControllers();
        service.Explorer();
        return service;
    }

    static IServiceCollection Explorer(this IServiceCollection service)
    {
        service.AddDbContext<P2pokerDbContext>();
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen();
        return service;
    }

    public static WebApplication UseDocumentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }
}