namespace P2Poker.Interfaces;

public interface IBaseController
{
    void OnStart();

    void SetClient(string clientId);

    void SendRoomInfo(string clientId);

    void SetStartGame(string clientId);

    public void OnMessageFullRoom(string clientId);

    void SendTurn(int playerTurn);

    void NextTurn();

    void OnPot(string client, string[] info);
}