using P2PokerAPI.P2Poker.Core;

namespace P2Poker.Strucs;

public struct PlayerActionAndName
{
    public PlayerActionAndName(string playerName, PlayerAction action)
    {
        this.PlayerName = playerName;
        this.Action = action;
    }

    public string PlayerName { get; }

    public PlayerAction Action { get; }
}