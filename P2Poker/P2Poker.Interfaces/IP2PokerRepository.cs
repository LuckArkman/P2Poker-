using P2Poker.Bean;

namespace P2Poker.Interfaces;

public interface IP2PokerRepository : IGenericRepository<Room>, ISearchableRepository<Room>
{
    Task<Room?> Update(Room _room, CancellationToken cancellationToken);
}