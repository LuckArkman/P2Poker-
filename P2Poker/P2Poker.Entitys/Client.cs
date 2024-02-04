using System.Net.Sockets;
using System.Text;
using P2Poker.Bean;
using P2Poker.Dao;
using P2Poker.Enums;
using P2Poker.Exceptions;
using P2Poker.Interfaces;
using P2Poker.Singletons;
using P2PokerAPI.P2Poker.Core;

namespace P2Poker.Entitys;

public class Client : ClientDAO, IPlayer
{
    public bool IsHouseOwner()
        => roomController.IsPlayerWinner(this);

    public void Send(byte[] bytes)
    {
        try
        {
            if (bytes is not null && client.Connected) client.Send(bytes);
        }
        catch (SocketException e)
        {
            if (e.NativeErrorCode.Equals(10035)) server.RemoveClient(this, this.socket);
            if (e.NativeErrorCode.Equals(10054)) server.RemoveClient(this, this.socket);
            if (e.ErrorCode.Equals(32)) server.RemoveClient(this, this.socket);
        }
        finally
        {
            
        }
    }

    public IStartHandContext handContext { get; set; }
    public Guid UserID => UUID;

    public int BuyIn => coins;
    public Room roomController { get; set; }

    public Socket socket => client;

    public int handNumber { get; set; }

    public int PlayerCoins => coins;

    public IStartGameContext startGameContext { get; set; }
    public int PlayerNumber { get; set; }

    public Client(Socket clientSocket, Server sv)
    {
        this.client = clientSocket;
        this.server = sv;
        this.UUID = Guid.NewGuid();
    }

    public void SetServer(Server sv) => this.server = sv;

    public void OnStart()
    {
        receiveDone = new ManualResetEvent(false);
        controllerManager = new ControllerManager();
        if (client == null || !client.Connected) return;
        HandleClient();
    }
    public void SendData(object? obj)
    {
        Send((byte[])obj);
    }

    void HandleClient()
    {
        while (client.Connected)
        {
            try
            {

                client.BeginReceive(buffer, 0, 1024, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
                
            }
            catch (SocketException e)
            {
                if (e.NativeErrorCode.Equals(10035)) server.RemoveClient(this, this.socket);
                if (e.NativeErrorCode.Equals(10054)) server.RemoveClient(this, this.socket);
                if (e.ErrorCode.Equals(32)) server.RemoveClient(this, this.socket);
            }
            finally
            {
            }
        }

    }

    public void ReceiveCallback(IAsyncResult ar)
    {
        Message msg = new Message();
        try
        {
            int count = client.EndReceive(ar);
            if (count > 0)
            {
                msg.ReadMessage(Encoding.UTF8.GetString(buffer, 0, count), OnProcessMessage);
                client.BeginReceive(buffer, 0, 1024, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
        }
        catch (SocketException e)
        {
            if (e.NativeErrorCode.Equals(10035)) server.RemoveClient(this, this.socket);
            if (e.NativeErrorCode.Equals(10054)) server.RemoveClient(this, this.socket);
            if (e.ErrorCode.Equals(32)) server.RemoveClient(this, this.socket);
        }
        finally{}
    }

    public Room? OnJoinRoom(IPlayer client, Guid guid)
    {
        var db = Singleton._singleton().CreateDBContext();
        var reposit = Singleton._singleton().CreateRoomRepository(db);
        var rom = reposit.Get(guid);
        NotFoundException.ThrowIfNull(rom, $"Room '{guid}' not found.");
        if (rom.clientList.Count() < 6) rom.JoinClient(client);
        roomController = rom;
        return rom!;
    }

    private void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
    =>server.HandleRequest(requestCode, actionCode, data, this);

    public void SetRom(Room room) => roomController = room;


    public void OnCreateRoom(string[] info)
    {
    }

    public void OnStartGame(string[] info)
    {
    }

    public void StartHand(IStartHandContext context) => handContext = context;

    public void StartRound(IStartRoundContext context)
    {
    }

    public PlayerAction PostingBlind(IPostingBlindContext context) => new PlayerAction(PlayerActionType.Fold);

    public PlayerAction GetTurn(IGetTurnContext context) => new PlayerAction(PlayerActionType.Fold);

    public void EndRound(IEndRoundContext context)
    {
    }

    public void EndHand(IEndHandContext context)
    {
    }

    public void EndGame(IEndGameContext context)
    {
    }

    public void StartGame(IStartGameContext context) => startGameContext = context;

    public void clientGO()
    {
    }

    public void OnTurn(string[] info) => roomController.NextTurn();
}