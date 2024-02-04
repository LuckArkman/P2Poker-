using P2Poker.Bean;

namespace P2Poker.Interfaces;

public interface IP2PokerRepository : IGenericRepository<Room>, ISearchableRepository<Room>
{
    Task Update(Room _room);
}