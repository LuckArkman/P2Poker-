using System.Net;
using System.Net.Sockets;
using P2Poker.Bean;
using P2Poker.Dao;
using P2Poker.Enums;
using P2Poker.Interfaces;
using P2Poker.Singletons;

namespace P2Poker.Entitys;

public class Server : ServerDAO
{
    public Server(){}

    public Server(string host, int port)
    {
        controllerManager = new ControllerManager(this);
        connectDone = new ManualResetEvent(false);
        receiveDone = new ManualResetEvent(false);
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
        Start(ipEndPoint);
    }

    void Start(IPEndPoint ipEndPoint)
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(ipEndPoint);
        socket.Listen(100);
        Console.WriteLine($"Seed Is Started");
        Thread acceptThread = new Thread(AcceptConnections);
        acceptThread.Start();
    }

    void AcceptConnections()
    {
        socket.BeginAccept(ConnectCallback, null);
    }

    void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = socket.EndAccept(ar);
            Client cl = new Client(client, this);
            cl.OnStart();
            cl.SendData(Message.PackData(new Msg(RequestCode.User, ActionCode.UserId, cl.UserID.ToString())));
            clientList.Add(cl.UserID, cl);
            socket.BeginAccept(ConnectCallback, null);
        }
        catch (SocketException e)
        {
            Console.WriteLine("In Server ::> SocketException " + e.ToString());
        }
    }

    public void SendResponse(Guid RoomId,IPlayer client, ActionCode actionCode, string data)
    {
        var db = Singleton._singleton().CreateDBContext();
        var reposit = Singleton._singleton().CreateRoomRepository(db);
        var rom = reposit.Get(RoomId);
        var cl = rom.clientList.Find(c => c.UserID == client.UserID);
    }
}