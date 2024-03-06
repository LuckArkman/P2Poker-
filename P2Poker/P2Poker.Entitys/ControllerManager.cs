using System.Reflection;
using P2Poker.Bean;
using P2Poker.Enums;
using P2Poker.Exceptions;
using P2Poker.Interfaces;

namespace P2Poker.Entitys;

public class ControllerManager : IControllerManager
{
    readonly Dictionary<RequestCode, BaseController> _controllerDict = new Dictionary<RequestCode, BaseController>();
    GameController? _gameController { get; set; }
    Room? _room { get; set; }
    RoomController? _roomController { get; set; }
    Server server { get; set; }

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
        _controllerDict.Add(defaultController.RequestCode, new DefaultController());
        _controllerDict.Add(RequestCode.User, new UserController());
        _controllerDict.Add(RequestCode.Room, new RoomController());
        _controllerDict.Add(RequestCode.Game, new GameController());
    }

    public async void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, IPlayer client)
    {
        BaseController controller;
        bool isGet = _controllerDict.TryGetValue(requestCode, out controller);
        NotFoundException.ThrowIfNull(isGet, $"isGet '{requestCode}' Can't found controller for.");
        if (actionCode is ActionCode.CreateRoom) _room = await new RoomController().CreateRoom(client);
        if (actionCode is ActionCode.ListRoom) _room = client.getRoom();
        if (actionCode is ActionCode.JoinRoom) _room = await client.OnJoinRoom(client, new Guid(data));
        if (actionCode is ActionCode.StartGame) _gameController = client.roomController.GameController();
        if (actionCode is ActionCode.Bet) _gameController = client.roomController.GameController();
        if (actionCode is ActionCode.Cobrir) _gameController = client.roomController.GameController();
        if (actionCode is ActionCode.Pass) _gameController = client.roomController.GameController();
        if (actionCode is ActionCode.Check) _gameController = client.roomController.GameController();
        if (_room is not null) _roomController = new RoomController();
        if (_roomController is not null) _roomController.OnRoom(requestCode,actionCode, client, _room);
        if (_gameController is not null) _gameController.Game(actionCode, client, data);
        await Task.CompletedTask;
    }
}