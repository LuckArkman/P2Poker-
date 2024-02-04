using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P2Poker.Context;
using P2Poker.Entitys;
using P2Poker.Singletons;

Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
{
    Server server = new Server(IPAddress.Any.ToString(), 7777);    
    services.AddHostedService<ConsoleWriter>();
    services.AddDbContext<P2pokerDbContext>();
}).Build().Run();