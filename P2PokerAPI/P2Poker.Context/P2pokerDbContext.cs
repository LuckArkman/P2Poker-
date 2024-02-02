using Microsoft.EntityFrameworkCore;
using P2Poker.Bean;

namespace P2Poker.Context;

public class P2pokerDbContext : DbContext
{
    public Dictionary<Guid, Room> _rooms = new Dictionary<Guid, Room>();
}