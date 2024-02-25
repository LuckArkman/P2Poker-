using System.Net.Sockets;
using P2Poker.Entitys;
using P2Poker.Enums;

namespace P2Poker.Interfaces;

public interface IServer
{
    void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, IPlayer client);
    void RemoveClient(IPlayer player, Socket socket);
    void CloseConnection(IPlayer player, Socket socket);
}