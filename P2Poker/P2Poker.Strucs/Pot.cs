namespace P2Poker.Strucs;

public struct Pot
{
    public Pot(string id, int amountOfMoney, IReadOnlyList<string> activePlayer)
    {
        this.UUID = id;
        this.AmountOfMoney = amountOfMoney;
        this.ActivePlayer = activePlayer;
    }

    public string UUID { get; set; }

    public int AmountOfMoney { get; set; }

    public IReadOnlyList<string> ActivePlayer { get; set; }
}