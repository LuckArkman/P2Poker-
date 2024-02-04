using P2PokerAPI.P2Poker.Core;

namespace P2Poker.Interfaces;

public interface IPostingBlindContext
{
    PlayerAction BlindAction { get; }
    int CurrentStackSize { get; }
    int CurrentPot { get; }
}