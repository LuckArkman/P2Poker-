using P2Poker.Singletons;

namespace P2Poker.Controller;

public static class ServerConfiguration
{
    public static IServiceCollection DBConfiguration(this IServiceCollection service)
        => service.ServerInfo();
    
    static IServiceCollection ServerInfo(this IServiceCollection service)
    {
        service.AddSingleton<Singleton>();
        return service;
    }
}