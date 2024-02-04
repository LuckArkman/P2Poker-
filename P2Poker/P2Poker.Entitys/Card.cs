using P2Poker.Interfaces;

namespace P2Poker.Entitys;

public class Card : ICard
{
    public Card(){}
    public Guid UUID { get; set; }
    public int value { get; set; }

    public Card(Guid uuid, int _value)
    {
        UUID = uuid;
        this.value = _value;
    }
}