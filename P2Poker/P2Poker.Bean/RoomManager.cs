using P2Poker.Interfaces;

namespace P2Poker.Bean;

public class RoomManager
{
    public IStartTableContext tableContext { get; set; }
    public Guid uuid { get; set; }
    public int playerButton { get; set; }
    public int turn { get; set; }
    
    public RoomManager(IStartTableContext tableContext, Guid uuid, int playerButton, int turn)
    {
        this.tableContext = tableContext;
        this.uuid = uuid;
        this.playerButton = playerButton;
        this.turn = turn;
    }
}