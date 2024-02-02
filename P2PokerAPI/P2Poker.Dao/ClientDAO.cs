using System.Net.Sockets;
using P2Poker.Interfaces;
using IServer = Microsoft.AspNetCore.Hosting.Server.IServer;

namespace P2Poker.Dao;

public class ClientDAO
{
    public ServerDAO server { get; set; }
    public int coins = 0;
    public Guid UUID { get; set; }
    public Socket client { get; set; }
    public static ManualResetEvent? connectDone, receiveDone;
    public static IControllerManager? controllerManager;
    public IRoomController roomController { get; set; }
    public String response = String.Empty;
    public int PlayerNumber { get; set; }
    public byte[] buffer = new byte[1024];

    public void SetBuffer(byte[] buffer) => this.buffer = buffer;
    public void SetSocket(Socket socket) => this.client = socket;
        
    public void SendMessage(byte[] bytes)
    {
    }

    //public void OnBetRoom(string[] info) => roomController.OnPot(UUID, info);
}