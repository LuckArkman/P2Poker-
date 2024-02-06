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

    public string JoinRoom(string data, Client client, Server server)
    {
        return "";
    }

    public void CreateRoom(IPlayer client)
    {
        var db = Singleton._singleton().CreateDBContext();
        var repository = Singleton._singleton().CreateRoomRepository(db);
        var output = new Room();
        output.OnStart();
        repository.Insert(output);
        Room room = repository.Get(output.UUID);
        NotFoundException.ThrowIfNull(room, $"isGet '{room}' Can't found controller for.");
        room.SendMessage(client, RequestCode.Room, ActionCode.CreateRoom, output.UUID.ToString());
    }

    public void ListRooms(IPlayer client)
    {
        var db = Singleton._singleton().CreateDBContext();
        var repository = Singleton._singleton().CreateRoomRepository(db);
        try
        {
            client.SendData(
                Message.PackData(
                    new Msg(
                        RequestCode.Room,
                        ActionCode.ListRoom,
                        JsonConvert.SerializeObject(repository.GetRooms()
                        )
                    )
                )
            );
        }
        catch (JsonException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}