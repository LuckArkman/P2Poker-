using System.Net;
using System.Net.Sockets;
using P2Poker.Enums;
using P2Poker.Interfaces;
using P2Poker.Singletons;

namespace P2Poker.Dao;

public class ServerDAO
{
    public IPAddress ipAddress;
    public IPEndPoint ipEndPoint;
    public Socket socket;
    public IControllerManager controllerManager;
    public static ManualResetEvent connectDone, receiveDone;
    public List<RoomControllerDAO> roomControllers = new List<RoomControllerDAO>();
    public Dictionary<Guid,IPlayer> clientList = new Dictionary<Guid, IPlayer>();
    public byte[] buffer = new byte[1024];
}