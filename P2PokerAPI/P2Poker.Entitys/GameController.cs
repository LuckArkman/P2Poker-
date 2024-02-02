using P2Poker.Bean;
using P2Poker.Enums;
using P2Poker.Interfaces;

namespace P2Poker.Entitys;

public class GameController : BaseController
{
    public GameController()
        => requestCode = RequestCode.Game;
    
    public string StartGame(string data, IPlayer client)
    {
        if (client.IsHouseOwner())
        {
            var room = client.roomController;
            room.BroadCastMessage(client, RequestCode.Game,ActionCode.StartGame, ((int)ReturnCode.Success).ToString());
            room.StartTimer();
            return ((int)ReturnCode.Success).ToString();
        }
        else
        {
            return ((int)ReturnCode.Fail).ToString();
        }
    }
    public string Action(string data, IPlayer client, Server server)
    {
        var room = client.roomController;
        room.BroadCastMessage(client, RequestCode.Game,ActionCode.turn, data);
        return data;
    }
    public string Cobrir(string data, IPlayer client, Server server)
    {
        var room = client.roomController;
        room.BroadCastMessage(client, RequestCode.Game, ActionCode.Cobrir, data);
        return data;
    }
    public string Bet(string data, IPlayer client, Server server)
    {
        int damage = int.Parse(data);
        var room = client.roomController;
        room.TakeBet(damage,client);
        return data;
    }
}