using P2Poker.Bean;

namespace P2Poker.Interfaces;

public interface IGenericRepository<TAggregate> : IRepository
{
    public Task Insert( TAggregate tAggregate, CancellationToken cancellationToken);
    public Task<Room> Get(Guid Id, CancellationToken cancellationToken);
    
    public Task Delete(Guid Id, CancellationToken cancellationToken);
    
    public Task<Room?> Update(Room _room, CancellationToken cancellationToken);
    
}