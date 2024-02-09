using System.Runtime.InteropServices.ComTypes;
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

    public void JoinRoom(RequestCode requestCode, ActionCode actionCode, IPlayer client, Room? _room)
    {
        if (_room.clientList.Count > 5) _room = null;
        if (_room.clientList.Count <= 5) _room.JoinClient(client);
        var ls = _room.clientList.FindAll(x => x.UserID != client.UserID);
        var offCl = ls.FindAll(c => !c.socket.Connected);
        if (offCl.Count > 0) offCl.ToList().ForEach(x => x.Remove(x));
        var cls = ls.ToList().FindAll(x => x.socket.Connected);
        if (_room is not null)
        {
            if (cls.Count > 0)
            {
                SendUsersInRoom(cls, client, requestCode, actionCode, _room);
                SendUserInJoinRoom(cls, client, requestCode, actionCode, _room);
            }

            if (_room is null) NotJoin(client);
        }
    }

    private async void SendUsersInRoom(List<IPlayer> cls, IPlayer client, RequestCode requestCode, ActionCode actionCode, Room _room)
    {
        int i = 0;
        while (i < cls.Count)
        {
            await Task.Delay(50);
            _room!.SendMessage(client, requestCode, actionCode, cls[i].UserID.ToString());
            i++;
        }
    }

    private async void SendUserInJoinRoom(List<IPlayer> cls, IPlayer client, RequestCode requestCode, ActionCode actionCode, Room _room)
    {
        int i = 0;
        while (i < cls.Count)
        {
            await Task.Delay(50);
            _room!.SendMessage(cls[i], requestCode, actionCode, client.UserID.ToString());
            i++;
        }
    }

    private void NotJoin(IPlayer client)
    {
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
            client.SendData(Message.PackData(new Msg(RequestCode.Room, ActionCode.ListRoom,
                JsonConvert.SerializeObject(repository.GetRooms()))));
        }
        catch (JsonException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void OnRoom(RequestCode requestCode, ActionCode actionCode, IPlayer client, Room? room)
    {
        if (actionCode is ActionCode.CreateRoom)room!.SendMessage(client, RequestCode.Room, ActionCode.CreateRoom, room.GetUUID().ToString());
        if (actionCode is ActionCode.JoinRoom) JoinRoom(requestCode, actionCode, client, room);
        if (actionCode is ActionCode.ListRoom) ListRooms(client);
    }
}