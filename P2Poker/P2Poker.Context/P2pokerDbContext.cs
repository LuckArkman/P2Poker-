using Microsoft.EntityFrameworkCore;
using P2Poker.Bean;

namespace P2Poker.Context;

public class P2pokerDbContext : DbContext
{
    public DbSet<Room> _rooms => Set<Room>();
    public P2pokerDbContext(DbContextOptions<P2pokerDbContext> options) : base(options){}
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseMySql("Server=194.113.64.33;DataBase=Cursed Stone;Uid=root;Pwd=10nXwq60gOC8gRBgtO",
            ServerVersion.AutoDetect("Server=194.113.64.33;DataBase=Cursed Stone;Uid=root;Pwd=10nXwq60gOC8gRBgtO"));
            */
}