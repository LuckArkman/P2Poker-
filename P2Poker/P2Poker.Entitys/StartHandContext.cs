using P2Poker.Interfaces;

namespace P2Poker.Entitys;

public class StartHandContext : IStartHandContext
{
    public StartHandContext(Guid firstCard, Guid secondCard, Guid playerName, int handNumber, int moneyLeft)
    {
        this.firstCard = firstCard;
        this.secondCard = secondCard;
        PlayerName = playerName;
        HandNumber = handNumber;
        MoneyLeft = moneyLeft;
    }

    public Guid firstCard { get; set; }
    public Guid secondCard { get; set; }
    public Guid PlayerName { get; }
    public int HandNumber { get; set; }
    public int MoneyLeft { get; set; }
    public int SmallBlind { get; set; }
}