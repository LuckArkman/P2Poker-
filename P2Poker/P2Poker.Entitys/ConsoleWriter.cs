using Microsoft.Extensions.Hosting;

namespace P2Poker.Entitys;

public class ConsoleWriter : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"{DateTime.Now.ToString()} Server Is Only");
            await Task.Delay(60000);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        while (cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"{DateTime.Now} Server Is Sleep");
            await Task.CompletedTask;
        }
    }
}