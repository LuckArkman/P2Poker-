using P2Poker.Enums;
using P2Poker.Strucs;

namespace P2Poker.Interfaces;

public interface IGetTurnContext
{
    bool CanCheck { get; }

    bool CanRaise { get; }

    int CurrentMaxBet { get; }

    int CurrentPot { get; }

    bool IsAllIn { get; }

    int MoneyLeft { get; }

    int MoneyToCall { get; }

    int MyMoneyInTheRound { get; }

    IReadOnlyCollection<PlayerActionAndName> PreviousRoundActions { get; }

    GameRoundType RoundType { get; }

    int SmallBlind { get; }

    int MinRaise { get; }

    Pot MainPot { get; }

    IReadOnlyCollection<Pot> SidePots { get; }
}