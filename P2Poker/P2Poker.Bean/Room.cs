using System.Diagnostics;
using P2Poker.Dao;
using P2Poker.Entitys;
using P2Poker.Enums;
using P2Poker.Interfaces;

namespace P2Poker.Bean;

public class Room : RoomControllerDAO
{
    public Room()
    {
    }

    public GameController GameController()
    {
        if (_gameController is null)
        {
            _gameController = new GameController();
            return _gameController;
        }
        return _gameController;
    }

    public void OnStart()
    {
        if (UUID.ToString() == "00000000-0000-0000-0000-000000000000" || UUID == null)
        {
            UUID = Guid.NewGuid();
        }
    }

    public void SetClient(IPlayer cl) => client = cl;

    public void SendRoomInfo(IPlayer cl)
    {
    }

    public void SetStartGame(IPlayer client)
    {
    }

    public void OnMessageFullRoom(IPlayer client)
    {
    }

    async void SendTurn(int playerTurn)
        => await Task.CompletedTask;

    public void NextTurn() => SendTurn(turn);

    public void OnPot(string client, string[] info)
    {
    }

    public async void JoinClient(IPlayer player)
    {
        if (client is null) SetDealer(player);
        clientList.Add(player);
        player.SendData(Message.PackData(new Msg(RequestCode.Room, ActionCode.UserPosition, (clientList.Count -1).ToString())));
        await Task.Delay(50);
        player.SendData(Message.PackData(new Msg(RequestCode.User, ActionCode.JoinRoom, player.UserID.ToString())));

    }
    private async void SetDealer(IPlayer player)
    {
        if (client is null)
        {
            client = player;
            await Task.Delay(2000);
            player.SendData(Message.PackData(new Msg(RequestCode.Room, ActionCode.dealer, player.UserID.ToString())));
        }
        await Task.CompletedTask;
    }

    public void RemoveClient(IPlayer o)
    {
        var u = clientList.Find(c => c.UserID == o.UserID);
        Console.WriteLine(nameof(RemoveClient));
        clientList.ForEach(c =>
        {
            if (c.UserID == o.UserID)
            {
                lock (clientList)
                {
                    clientList.Remove(u);
                }
            }
        });
    }

    public List<IPlayer> GetClients()
        => clientList;

    public void StartTableCards()
    {
        turn = 0;
    }
}