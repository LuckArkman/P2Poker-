using System.Net;
using System.Net.Sockets;
using P2Poker.Enums;
using P2Poker.Interfaces;
using P2Poker.Singletons;

namespace P2Poker.Dao;

public class ServerDAO : IServer
{
    public IPAddress ipAddress;
    public IPEndPoint ipEndPoint;
    public Socket socket;
    public IControllerManager controllerManager;
    public static ManualResetEvent connectDone, receiveDone;
    public List<RoomControllerDAO> roomControllers = new List<RoomControllerDAO>();
    public Dictionary<Guid,IPlayer> clientList = new Dictionary<Guid, IPlayer>();
    public byte[] buffer = new byte[1024];
    
    public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, IPlayer client)
        => controllerManager.HandleRequest(requestCode, actionCode, data, client);

    public void RemoveClient(IPlayer player, Socket socket)
    {
        var db = Singleton._singleton().CreateDBContext();
        var reposit = Singleton._singleton().CreateRoomRepository(db);
        player.socket.Shutdown(SocketShutdown.Both);
        if (reposit.Get(player.roomController.UUID).clientList.Find(c => c.UserID == player.UserID) is not null)
        {
            lock (reposit.Get(player.roomController.UUID).clientList)
            {
                reposit.Get(player.roomController.UUID).clientList.Remove(player);
                Console.WriteLine($"{nameof(RemoveClient)} In Server Cliente Id {player.UserID} has be desconnected");
            }
        }
    }
}