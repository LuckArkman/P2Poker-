using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P2Poker.Context;
using P2Poker.Entitys;

Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
{
    Server server = new Server(IPAddress.Any.ToString(), 7777);
    services.AddDbContext<P2pokerDbContext>(options => options.UseInMemoryDatabase("P2pokerDbContext-Memory"));
    /*
    new InicializaBD().Initialize(new CursedStoneDBContext(
        new DbContextOptionsBuilder<CursedStoneDBContext>()
            .UseMySql("Server=194.113.64.33;DataBase=Cursed Stone;Uid=root;Pwd=10nXwq60gOC8gRBgtO",ServerVersion.AutoDetect("Server=194.113.64.33;DataBase=Cursed Stone;Uid=root;Pwd=10nXwq60gOC8gRBgtO"))
            .Options));
    */
    services.AddHostedService<ConsoleWriter>();
}).Build().Run();