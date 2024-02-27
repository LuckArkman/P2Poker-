using P2Poker.Enums;

namespace P2Poker.Interfaces;

public interface IRoomController
{
    bool IsPlayerWinner(IPlayer clientDao);
    void BroadCastMessage(IPlayer player, RequestCode requestCode, ActionCode actionCode, string message);
    void SendMessage(IPlayer player, RequestCode requestCode, ActionCode actionCode, string message);
    Guid GetUUID();
}