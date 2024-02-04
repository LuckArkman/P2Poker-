using System.Reflection;
using P2Poker.Bean;
using P2Poker.Enums;
using P2Poker.Exceptions;
using P2Poker.Interfaces;

namespace P2Poker.Entitys;

public class ControllerManager : IControllerManager
{
    private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
    private Server server;

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

        string methodName = Enum.GetName(typeof(ActionCode), actionCode);
        MethodInfo mi = controller.GetType().GetMethod(methodName);
        NotFoundException.ThrowIfNull(mi, $"methodName '{mi}' there is no corresponding processing method.");

        var room = actionCode is ActionCode.JoinRoom ? client.OnJoinRoom(client, new Guid(data)) : null;
        if (room is not null && room.clientList.Count >= 1)
        {
            room.BroadCastMessage(client, requestCode, actionCode, client.UserID.ToString());
            var ls = from cl in room.clientList where cl.UserID != client.UserID && cl.socket.Connected select cl;
            var cls = ls.ToList();
            int i = 0;
            while (i < ls.Count())
            {
                await Task.Delay(50);
                if(cls[i].socket.Connected) room.SendMessage(client, RequestCode.Room, ActionCode.JoinRoom, cls[i].UserID.ToString());
                if (!cls[i].socket.Connected)
                {
                    lock (cls)
                    {
                        cls.Remove(cls[i]);
                    }
                }
                i++;
            }

            int c = 0;
            while (c < ls.Count())
            {
                await Task.Delay(50);
                if(cls[c].socket.Connected) room.SendMessage(cls[c], RequestCode.User, actionCode, client.UserID.ToString());
                if (!cls[c].socket.Connected)
                {
                    lock (cls)
                    {
                        cls.Remove(cls[c]);
                    }
                }
                c++;
            }
        }
        if (room is not null && room.clientList.Count < 1) room.SendMessage(client, requestCode, actionCode, client.UserID.ToString());

        await Task.CompletedTask;
    }
}