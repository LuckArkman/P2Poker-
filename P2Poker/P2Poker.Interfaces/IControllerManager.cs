using P2Poker.Enums;

namespace P2Poker.Interfaces;

public interface IControllerManager
{
    void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, IPlayer client);
}