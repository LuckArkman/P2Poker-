using P2Poker.Strucs;

namespace P2Poker.Interfaces;

public interface IEndRoundContext
{
    IReadOnlyCollection<PlayerActionAndName> RoundActions { get; }
}