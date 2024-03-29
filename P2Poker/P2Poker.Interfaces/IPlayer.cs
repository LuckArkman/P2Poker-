using System.Net.Sockets;
using P2Poker.Bean;
using P2PokerAPI.P2Poker.Core;

namespace P2Poker.Interfaces;

public interface IPlayer
{
    IStartHandContext handContext { get; set; }
    IStartGameContext startGameContext { get; set; }
    int PlayerNumber { get; set; }
    int handNumber { get; set; }
    Guid UserID { get; }
    Socket socket { get; }
    int PlayerCoins { get; }
    int BuyIn { get; }
    Room roomController { get; set; }
    Room? getRoom();
    void StartGame(IStartGameContext context);
    void StartHand(IStartHandContext context);
    void StartRound(IStartHandContext context);
    PlayerAction PostingBlind(IPostingBlindContext context);
    PlayerAction GetTurn(IGetTurnContext context);
    void EndRound(IEndRoundContext context);
    void EndHand(IEndHandContext context);
    void EndGame(IEndGameContext context);
    void SendMessage(byte[] bytes);
    bool IsHouseOwner();
    void Send(byte[] bytes);
    void SendData(object? obj);
    Task<Room> OnJoinRoom(IPlayer client, Guid guid);
    void Remove(IPlayer player);
    bool _openbet { get; set; }
    bool OpenBet();
}