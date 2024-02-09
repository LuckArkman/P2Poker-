namespace P2Poker.Interfaces;

public interface IStartHandContext
{
    public Guid firstCard { get; set; }

    public Guid secondCard { get; set; }

    Guid PlayerName { get; }

    int HandNumber { get; set; }

    int MoneyLeft { get; set; }

    int SmallBlind { get; set; }
}