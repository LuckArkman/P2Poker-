using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;
using P2Poker.Bean;
using P2Poker.Dao;
using P2Poker.Enums;
using P2Poker.Exceptions;
using P2Poker.Interfaces;
using P2Poker.Singletons;

namespace P2Poker.Entitys;

public class RoomController : BaseController
{
    public RoomController()
    => requestCode = RequestCode.Room;

    public async void JoinRoom(RequestCode requestCode, ActionCode actionCode, IPlayer client, Room? room)
    {
        Room? _room = null;
        if (room.clientList.Count > 5) _room = null;
        if (room.clientList.Count <= 5) _room = room;
        if (_room is not null)
        {
            var ls = _room.clientList.FindAll(x => x._guid != client.UserID);
            var offCl = ls.FindAll(c => !c._player.socket.Connected);
            if (offCl.Count > 0) offCl.ForEach(of => { _room.RemoveClient(of._player); });
            _room.JoinClient(client);
            var cls = ls.FindAll(x => x._player.socket.Connected);
            if (cls.Count > 0)
            {
                var s = _room.clientList.FindAll(x => x._guid != client.UserID);
                await SendUsersInRoom(s, client, requestCode, actionCode, _room);
                await SendUserInJoinRoom(s, client, requestCode, actionCode, _room);
            }
        }
        if (_room is null) NotJoin(client);
    }

    private async Task SendUsersInRoom(List<UserClients> cls, IPlayer client, RequestCode requestCode, ActionCode actionCode, Room _room)
    {
        int i = 0;
        while (i < cls.Count)
        {
            await Task.Delay(50);
            _room!.SendMessage(client, requestCode, actionCode, cls[i]._guid.ToString());
            i++;
        }
        await Task.CompletedTask;
    }

    private async Task SendUserInJoinRoom(List<UserClients> cls, IPlayer client, RequestCode requestCode, ActionCode actionCode, Room _room)
    {
        int i = 0;
        while (i < cls.Count)
        {
            await Task.Delay(50);
            _room!.SendMessage(cls[i]._player, requestCode, actionCode, client.UserID.ToString());
            i++;
        }
        await Task.CompletedTask;
    }

    private void NotJoin(IPlayer client)
    {
    }

    public async Task<Room> CreateRoom(IPlayer client)
    {
        var db = Singleton._singleton().CreateDBContext();
        var repository = Singleton._singleton().CreateRoomRepository(db);
        var output = new Room();
        output.OnStart();
        await repository.Insert(output, CancellationToken.None);
        Room? room = await repository.Get(output.UUID, CancellationToken.None);
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
        if (actionCode is ActionCode.CreateRoom)
            room!.SendMessage(client, RequestCode.Room, ActionCode.CreateRoom, room.GetUUID().ToString());
        if (actionCode is ActionCode.JoinRoom) JoinRoom(requestCode, actionCode, client, room);
        if (actionCode is ActionCode.ListRoom) ListRooms(client);
    }
}