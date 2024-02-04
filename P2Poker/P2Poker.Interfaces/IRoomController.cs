namespace P2Poker.Interfaces;

public interface IRoomController
{
    bool IsPlayerWinner(IPlayer clientDao);
}