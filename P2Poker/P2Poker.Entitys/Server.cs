using System.Net;
using System.Net.Sockets;
using P2Poker.Bean;
using P2Poker.Dao;
using P2Poker.Enums;
using P2Poker.Interfaces;
using P2Poker.Singletons;

namespace P2Poker.Entitys;

public class Server : ServerDAO, IServer
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
        socket.BeginAccept(ConnectCallback, null);
    }

    void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = socket.EndAccept(ar);
            Client cl = new Client(client, this);
            cl.OnStart();
            clientList.Add(cl.UserID, cl);
            socket.BeginAccept(ConnectCallback, null);
        }
        catch (SocketException e)
        {
            Console.WriteLine("In Server ::> SocketException " + e.ToString());
        }
    }

    public async void SendResponse(Guid RoomId,IPlayer client, ActionCode actionCode, string data)
    {
        var db = Singleton._singleton().CreateDBContext();
        var reposit = Singleton._singleton().CreateRoomRepository(db);
        var rom = await reposit.Get(RoomId, CancellationToken.None);
        if(rom is not null)
        {
            IPlayer? cl = rom.GetClients().Find(c => c.UserID == client.UserID);
        }
    }

    public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, IPlayer client)
    => controllerManager.HandleRequest(requestCode, actionCode, data, client);

    public void RemoveClient(IPlayer player, Room _room)
    {
        player.socket.Shutdown(SocketShutdown.Both);
        var u = _room.clientList.Find(c => c.UserID == player.UserID);
        if (_room.clientList.Find( x => x.UserID == player.UserID) is not null)
        {
            lock (_room.clientList)
            {
                _room.clientList.Remove(u);
                Console.WriteLine($"{player.UserID} has be desconnected");
            }
        }
    }

    public void CloseConnection(IPlayer player, Socket socket)
    {
        player.socket.Shutdown(SocketShutdown.Both);
    }
}