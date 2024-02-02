using System.Net;

namespace P2Poker.Entitys;

public class Server
{
    public Server(){}

    public Server(string host, int port)
    {
        //controllerManager = new ControllerManager(this);
        //connectDone = new ManualResetEvent(false);
        //receiveDone = new ManualResetEvent(false);
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
        Start(ipEndPoint);
    }

    private void Start(IPEndPoint ipEndPoint)
    {
        
    }
}