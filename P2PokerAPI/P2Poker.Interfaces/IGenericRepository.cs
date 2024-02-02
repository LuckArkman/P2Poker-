using P2Poker.Bean;

namespace P2Poker.Interfaces;

public interface IGenericRepository<TAggregate> : IRepository
{
    public Task Insert( TAggregate tAggregate);
    public Room Get(Guid Id);
    
    public Task Delete(Guid Id);
    
    public Task Update(Room _room);
    
}