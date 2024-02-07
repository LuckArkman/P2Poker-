using Newtonsoft.Json;
using P2Poker.Bean;
using P2Poker.Enums;
using P2Poker.Exceptions;
using P2Poker.Interfaces;
using P2Poker.Singletons;

namespace P2Poker.Entitys;

public class RoomController : BaseController
{
    public RoomController()
    {
        requestCode = RequestCode.Room;
    }

    public void JoinRoom(IPlayer client, Room? _room)
    {
        if(_room.clientList.Count < 6) _room.JoinClient(client);
        _room.BroadCastMessage(client, requestCode, ActionCode.JoinRoom, client.UserID.ToString());
        var ls = from cl in _room.clientList where cl.UserID != client.UserID select cl;
        var offCl = from of in ls.ToList() where !of.socket.Connected select of;
        offCl.ToList().ForEach(x => x.Remove(x));
        var cls = from o in ls.ToList() where o.socket.Connected select o;
        if (cls.Count() <= 1) client.SendData(Message.PackData(new Msg(RequestCode.User, ActionCode.JoinRoom, client.UserID.ToString())));
        if (cls.Count() > 1)
        {
            foreach (var pl in cls)
            {
                if (pl.UserID != client.UserID)
                    _room.SendMessage(client, RequestCode.Room, ActionCode.JoinRoom, pl.UserID.ToString());
            }

            foreach (var pl in cls)
            {
                if (pl.UserID != client.UserID)
                    _room.SendMessage(pl, RequestCode.Room, ActionCode.JoinRoom, client.UserID.ToString());
            }
        }
    }

    public Room? CreateRoom(IPlayer client)
    {
        var db = Singleton._singleton().CreateDBContext();
        var repository = Singleton._singleton().CreateRoomRepository(db);
        var output = new Room();
        output.OnStart();
        repository.Insert(output);
        Room room = repository.Get(output.UUID);
        NotFoundException.ThrowIfNull(room, $"isGet '{room}' Can't found controller for.");
        return room;
    }
    public void ListRooms(IPlayer client)
    {
        var db = Singleton._singleton().CreateDBContext();
        var repository = Singleton._singleton().CreateRoomRepository(db);
        try
        {
            client.SendData(Message.PackData(new Msg(RequestCode.Room, ActionCode.ListRoom, JsonConvert.SerializeObject(repository.GetRooms()))));
        }
        catch (JsonException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public void OnRoom(ActionCode actionCode, IPlayer client, Room? room)
    {
        if (actionCode is ActionCode.CreateRoom) room!.SendMessage(client, RequestCode.Room, ActionCode.CreateRoom, room.GetUUID().ToString());
        if (actionCode is ActionCode.JoinRoom) JoinRoom(client, room);
        if (actionCode is ActionCode.ListRoom) ListRooms(client);
    }
}