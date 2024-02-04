namespace P2Poker.Interfaces;

public interface IStartTableContext
{
    public Guid firstCard { get; set; }

    public Guid secondCard { get; set; }

    public Guid ThirdCard { get; set; }

    public Guid FourCard { get; set; }
        
    public Guid FiveCard { get; set; }

    int HandNumber { get; }

    int MoneyLeft { get; }
}