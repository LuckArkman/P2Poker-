namespace P2Poker.Interfaces;

public interface ISearchableRepository<Taggregate>
{
    Task<Taggregate> Search(Guid input);
}