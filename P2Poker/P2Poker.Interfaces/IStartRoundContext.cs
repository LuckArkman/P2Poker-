using P2Poker.Enums;
using P2Poker.Strucs;

namespace P2Poker.Interfaces;

public interface IStartRoundContext
{
    IReadOnlyCollection<Guid> CommunityCards { get; }

    int CurrentPot { get; }

    int MoneyLeft { get; }

    GameRoundType RoundType { get; }

    Pot CurrentMainPot { get; }

    IReadOnlyCollection<Pot> CurrentSidePots { get; }
}