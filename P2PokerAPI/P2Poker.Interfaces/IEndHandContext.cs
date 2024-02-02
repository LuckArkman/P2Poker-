namespace P2Poker.Interfaces;

public interface IEndHandContext
{
    Dictionary<string, ICollection<Guid>> ShowdownCards { get; }
}