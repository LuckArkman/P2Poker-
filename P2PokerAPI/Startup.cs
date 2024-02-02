using System.Net;
using P2Poker.Host;

namespace P2PokerAPI;

public class Startup
{
    public Startup(){}
    public void StartHost()
    {
            
        new HostBuilder().ConfigureServices((hostBuilderContext, services) =>
        {
            services.AddHostedService<HostedSeed>();
        }).RunConsoleAsync();
    }
}