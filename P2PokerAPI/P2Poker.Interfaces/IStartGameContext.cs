namespace P2Poker.Interfaces;

public interface IStartGameContext
{
    IReadOnlyCollection<IPlayer> Players { get; }

    int StartMoney { get; }
}