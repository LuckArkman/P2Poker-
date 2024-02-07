using P2Poker.Bean;
using P2Poker.Enums;
using P2Poker.Interfaces;

namespace P2Poker.Entitys;

public class GameController : BaseController
{
    public Dictionary<Guid,IPlayer> clientsGO = new Dictionary<Guid,IPlayer>();
    public GameController()
        => requestCode = RequestCode.Game;
    
    public void StartGame(List<IPlayer> clientsGo, IPlayer client, Room? room)
    {
        if (client.UserID == room.client.UserID && clientsGO.Count >= 3) OnStartGame(clientsGO);
        clientsGO.Add(client.UserID, client);
        if(clientsGO.Count >=3) room.client.SendData(Message.PackData(new Msg(RequestCode.User, ActionCode.StartGame, "")));
    }

    private void OnStartGame(Dictionary<Guid, IPlayer> clientsGo)
    {
        
    }

    public void Action( IPlayer client,string data)
    {
    }
    public void Cobrir( IPlayer client,string data)
    {
    }
    public void Bet(IPlayer client,string data)
    {
    }

    public void Game(ActionCode actionCode, IPlayer client,  string data)
    {
        var room = client.roomController;
        if (actionCode == ActionCode.StartGame) StartGame(room.clientList, client, room);
        if (actionCode == ActionCode.Bet)Bet(client, data);
        if (actionCode == ActionCode.Cobrir)Cobrir(client, data);
        if (actionCode == ActionCode.Pass)Pass(client, data);
        if (actionCode == ActionCode.Check)Check(client, data);
    }

    private void Check(IPlayer client, string data)
    {
    }

    private void Pass(IPlayer client, string data)
    {
    }
}