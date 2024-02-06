using System.Reflection;
using P2Poker.Bean;
using P2Poker.Enums;
using P2Poker.Exceptions;
using P2Poker.Interfaces;

namespace P2Poker.Entitys;

public class ControllerManager : IControllerManager
{
    private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
    private Server server { get; set; }

    public ControllerManager()
    {
    }

    public ControllerManager(Server server)
    {
        this.server = server;
        Init();
    }

    private void Init()
    {
        DefaultController defaultController = new DefaultController();
        controllerDict.Add(defaultController.RequestCode, new DefaultController());
        controllerDict.Add(RequestCode.User, new UserController());
        controllerDict.Add(RequestCode.Room, new RoomController());
        controllerDict.Add(RequestCode.Game, new GameController());
    }

    public async void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, IPlayer client)
    {
        BaseController controller;
        bool isGet = controllerDict.TryGetValue(requestCode, out controller);
        NotFoundException.ThrowIfNull(isGet, $"isGet '{requestCode}' Can't found controller for.");

        if (actionCode is ActionCode.CreateRoom) new RoomController().CreateRoom(client);

        if (actionCode is ActionCode.ListRoom) new RoomController().ListRooms(client);
        Room? room = null;
        if (actionCode is ActionCode.JoinRoom) room = client.OnJoinRoom(client, new Guid(data));
        if (room is not null && room.clientList.Count > 1)
        {
            room.BroadCastMessage(client, requestCode, actionCode, client.UserID.ToString());
            var ls = from cl in room.clientList where cl.UserID != client.UserID select cl;
            var offCl = from of in ls.ToList() where !of.socket.Connected select of;
            offCl.ToList().ForEach(x => x.Remove(x));
            var cls = from o in ls.ToList() where o.socket.Connected select o;
            if (cls.Count() <= 1)client.SendData(
                Message.PackData(
                    new Msg(
                        RequestCode.User,
                        ActionCode.JoinRoom,
                    client.UserID.ToString())));
            if (cls.Count() > 1)
            {
                foreach (var pl in cls)
                {
                    if (pl.UserID != client.UserID) room.SendMessage(client, RequestCode.Room, ActionCode.JoinRoom, pl.UserID.ToString());
                }
                foreach (var pl in cls)
                {
                    if (pl.UserID != client.UserID) room.SendMessage(pl, RequestCode.Room, ActionCode.JoinRoom, client.UserID.ToString());
                }
            }
        }

        await Task.CompletedTask;
    }
}