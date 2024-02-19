using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P2Poker.Context;
using P2Poker.Entitys;

Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
{
    Server server = new Server(IPAddress.Any.ToString(), 7777);
    services.AddDbContext<P2pokerDbContext>();
    services.AddHostedService<ConsoleWriter>();
}).Build().Run();