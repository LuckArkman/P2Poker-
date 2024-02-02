using System.Net;
using System.Net.Sockets;
using P2Poker.Entitys;

namespace P2Poker.Host;

public class HostedSeed :IHostedService
{
    TcpListener listener {get;set;}
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Server server = new Server(IPAddress.Any.ToString(), 7777);
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}